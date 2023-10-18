using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TipoNotificacionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TipoNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TipoNotificacionDto>>>GetAllAsync()
        {
            var tipoNotificaciones = await _unitOfWork.TiposNotificaciones.GetAllAsync();
            return _mapper.Map<List<TipoNotificacionDto>>(tipoNotificaciones);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TipoNotificacionDto>>GetAllAsync(int id)
        {
            var tipoNotificacion = await _unitOfWork.TiposNotificaciones.GetByIdAsync(id);
            if(tipoNotificacion == null)
            {
                return BadRequest();
            }
            return _mapper.Map<TipoNotificacionDto>(tipoNotificacion);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoNotificacionDto>> Post(TipoNotificacionDto tipoNotificacionDto)
        {
            var tipoNotificacion = _mapper.Map<TipoNotificacion>(tipoNotificacionDto);
            if(tipoNotificacionDto.FechaCreacion == DateTime.MinValue)
            {
                tipoNotificacionDto.FechaCreacion = DateTime.Now;
                tipoNotificacion.FechaCreacion = DateTime.Now;
            }
            if(tipoNotificacionDto.FechaModificacion == DateTime.MinValue)
            {
                tipoNotificacionDto.FechaModificacion = DateTime.Now;
                tipoNotificacion.FechaModificacion = DateTime.Now;
            }
            this._unitOfWork.TiposNotificaciones.Add(tipoNotificacion);
            await _unitOfWork.SaveAsync();
            if(tipoNotificacion == null)
            {
                return BadRequest();
            }
            tipoNotificacionDto.Id = tipoNotificacion.Id;
            return CreatedAtAction(nameof(Post), new {id = tipoNotificacionDto.Id}, tipoNotificacionDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TipoNotificacionDto>> Put(int id, [FromBody] TipoNotificacionDto tipoNotificacionDto)
        {
            if(tipoNotificacionDto.Id == 0)
            {
                tipoNotificacionDto.Id = id;
            }
            if(tipoNotificacionDto == null)
            {
                return BadRequest();
            }
            tipoNotificacionDto.Id = id;
            var tipoNotificacion = _mapper.Map<TipoNotificacion>(tipoNotificacionDto);
            _unitOfWork.TiposNotificaciones.Update(tipoNotificacion);
            await _unitOfWork.SaveAsync();
            return tipoNotificacionDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(int id)
        {
            var tipoNotificacion = await _unitOfWork.TiposNotificaciones.GetByIdAsync(id);
            if(tipoNotificacion == null){
                return NotFound();
            }
            _unitOfWork.TiposNotificaciones.Remove(tipoNotificacion);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}