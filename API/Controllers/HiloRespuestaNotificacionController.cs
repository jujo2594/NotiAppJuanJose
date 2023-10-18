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
    public class HiloRespuestaNotificacionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HiloRespuestaNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<HiloRespuestaNotificacionDto>>>GetAllAsync()
        {
            var hiloRespuestaNotificaciones = await _unitOfWork.HilosRespuestasNotificaciones.GetAllAsync();
            return _mapper.Map<List<HiloRespuestaNotificacionDto>>(hiloRespuestaNotificaciones);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HiloRespuestaNotificacionDto>>GetAllAsync(int id)
        {
            var hiloRespuestaNotificacion = await _unitOfWork.HilosRespuestasNotificaciones.GetByIdAsync(id);
            if(hiloRespuestaNotificacion == null)
            {
                return BadRequest();
            }
            return _mapper.Map<HiloRespuestaNotificacionDto>(hiloRespuestaNotificacion);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HiloRespuestaNotificacionDto>> Post(HiloRespuestaNotificacionDto hiloRespuestaNotificacionDto)
        {
            var hiloRespuestaNotificacion = _mapper.Map<HiloRespuestaNotificacion>(hiloRespuestaNotificacionDto);
            if(hiloRespuestaNotificacionDto.FechaCreacion == DateTime.MinValue)
            {
                hiloRespuestaNotificacionDto.FechaCreacion = DateTime.Now;
                hiloRespuestaNotificacion.FechaCreacion = DateTime.Now;
            }
            if(hiloRespuestaNotificacionDto.FechaModificacion == DateTime.MinValue)
            {
                hiloRespuestaNotificacionDto.FechaModificacion = DateTime.Now;
                hiloRespuestaNotificacion.FechaModificacion = DateTime.Now;
            }
            this._unitOfWork.HilosRespuestasNotificaciones.Add(hiloRespuestaNotificacion);
            await _unitOfWork.SaveAsync();
            if(hiloRespuestaNotificacion == null)
            {
                return BadRequest();
            }
            hiloRespuestaNotificacionDto.Id = hiloRespuestaNotificacion.Id;
            return CreatedAtAction(nameof(Post), new {id = hiloRespuestaNotificacionDto.Id}, hiloRespuestaNotificacionDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HiloRespuestaNotificacionDto>> Put(int id, [FromBody] HiloRespuestaNotificacionDto hiloRespuestaNotificacionDto)
        {
            if(hiloRespuestaNotificacionDto.Id == 0)
            {
                hiloRespuestaNotificacionDto.Id = id;
            }
            if(hiloRespuestaNotificacionDto == null)
            {
                return BadRequest();
            }
            hiloRespuestaNotificacionDto.Id = id;
            var hiloRespuestaNotificacion = _mapper.Map<HiloRespuestaNotificacion>(hiloRespuestaNotificacionDto);
            _unitOfWork.HilosRespuestasNotificaciones.Update(hiloRespuestaNotificacion);
            await _unitOfWork.SaveAsync();
            return hiloRespuestaNotificacionDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(int id)
        {
            var hiloRespuestaNotificacion = await _unitOfWork.HilosRespuestasNotificaciones.GetByIdAsync(id);
            if(hiloRespuestaNotificacion == null){
                return NotFound();
            }
            _unitOfWork.HilosRespuestasNotificaciones.Remove(hiloRespuestaNotificacion);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}