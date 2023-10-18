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
    public class ModuloNotificacionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ModuloNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ModuloNotificacionDto>>>GetAllAsync()
        {
            var moduloNotificaciones = await _unitOfWork.ModulosNotificaciones.GetAllAsync();
            return _mapper.Map<List<ModuloNotificacionDto>>(moduloNotificaciones);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModuloNotificacionDto>>GetAllAsync(int id)
        {
            var moduloNotificacion = await _unitOfWork.ModulosNotificaciones.GetByIdAsync(id);
            if(moduloNotificacion == null)
            {
                return BadRequest();
            }
            return _mapper.Map<ModuloNotificacionDto>(moduloNotificacion);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ModuloNotificacionDto>> Post(ModuloNotificacionDto moduloNotificacionDto)
        {
            var moduloNotificacion = _mapper.Map<ModuloNotificacion>(moduloNotificacionDto);
            if(moduloNotificacionDto.FechaCreacion == DateTime.MinValue)
            {
                moduloNotificacionDto.FechaCreacion = DateTime.Now;
                moduloNotificacion.FechaCreacion = DateTime.Now;
            }
            if(moduloNotificacionDto.FechaModificacion == DateTime.MinValue)
            {
                moduloNotificacionDto.FechaModificacion = DateTime.Now;
                moduloNotificacion.FechaModificacion = DateTime.Now;
            }
            this._unitOfWork.ModulosNotificaciones.Add(moduloNotificacion);
            await _unitOfWork.SaveAsync();
            if(moduloNotificacion == null)
            {
                return BadRequest();
            }
            moduloNotificacionDto.Id = moduloNotificacion.Id;
            return CreatedAtAction(nameof(Post), new {id = moduloNotificacionDto.Id}, moduloNotificacionDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModuloNotificacionDto>> Put(int id, [FromBody] ModuloNotificacionDto moduloNotificacionDto)
        {
            if(moduloNotificacionDto.Id == 0)
            {
                moduloNotificacionDto.Id = id;
            }
            if(moduloNotificacionDto == null)
            {
                return BadRequest();
            }
            moduloNotificacionDto.Id = id;
            var moduloNotificacion = _mapper.Map<ModuloNotificacion>(moduloNotificacionDto);
            _unitOfWork.ModulosNotificaciones.Update(moduloNotificacion);
            await _unitOfWork.SaveAsync();
            return moduloNotificacionDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(int id)
        {
            var moduloNotificacion = await _unitOfWork.ModulosNotificaciones.GetByIdAsync(id);
            if(moduloNotificacion == null){
                return NotFound();
            }
            _unitOfWork.ModulosNotificaciones.Remove(moduloNotificacion);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}