using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EstadoNotificacionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EstadoNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EstadoNotificacionDto>>> GetAllAsync()
        {
            var estadosNotificaciones = await _unitOfWork.EstadosNotificaciones.GetAllAsync();
            return _mapper.Map<List<EstadoNotificacionDto>>(estadosNotificaciones);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EstadoNotificacionDto>>GetAllAsync(int id)
        {
            var estadoNotificacion = await _unitOfWork.EstadosNotificaciones.GetByIdAsync(id);
            if(estadoNotificacion == null)
            {
                return BadRequest();
            }
            return _mapper.Map<EstadoNotificacionDto>(estadoNotificacion);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EstadoNotificacionDto>> Post(EstadoNotificacionDto estadoNotificacionDto)
        {
            var estadoNotificacion = _mapper.Map<EstadoNotificacion>(estadoNotificacionDto);
            if(estadoNotificacionDto.FechaCreacion == DateTime.MinValue)
            {
                estadoNotificacionDto.FechaCreacion = DateTime.Now;
                estadoNotificacionDto.FechaCreacion = DateTime.Now;
            }
            if(estadoNotificacionDto.FechaModificacion == DateTime.MinValue)
            {
                estadoNotificacionDto.FechaModificacion = DateTime.Now;
                estadoNotificacion.FechaModificacion = DateTime.Now;
            }
            this._unitOfWork.EstadosNotificaciones.Add(estadoNotificacion);
            await _unitOfWork.SaveAsync();
            if(estadoNotificacion == null)
            {
                return BadRequest();
            }
            estadoNotificacionDto.Id = estadoNotificacion.Id;
            return CreatedAtAction(nameof(Post), new {id = estadoNotificacionDto.Id}, estadoNotificacionDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EstadoNotificacionDto>> Put(int id, [FromBody] EstadoNotificacionDto estadoNotificacionDto)
        {
            if(estadoNotificacionDto.Id == 0)
            {
                estadoNotificacionDto.Id = id;
            }
            if(estadoNotificacionDto == null)
            {
                return BadRequest();
            }
            estadoNotificacionDto.Id = id;
            var estadoNotificacion = _mapper.Map<EstadoNotificacion>(estadoNotificacionDto);
            _unitOfWork.EstadosNotificaciones.Update(estadoNotificacion);
            await _unitOfWork.SaveAsync();
            return estadoNotificacionDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var blockchain = await _unitOfWork.EstadosNotificaciones.GetByIdAsync(id);
            if(blockchain == null)
            {
                return NotFound();
            }
            _unitOfWork.EstadosNotificaciones.Remove(blockchain);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

    }
}