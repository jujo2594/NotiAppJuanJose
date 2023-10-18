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
    public class PermisoGenericoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PermisoGenericoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PermisoGenericoDto>>>GetAllAsync()
        {
            var permisoGenericos = await _unitOfWork.PermisosGenericos.GetAllAsync();
            return _mapper.Map<List<PermisoGenericoDto>>(permisoGenericos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PermisoGenericoDto>>GetAllAsync(int id)
        {
            var permisoGenerico = await _unitOfWork.PermisosGenericos.GetByIdAsync(id);
            if(permisoGenerico == null)
            {
                return BadRequest();
            }
            return _mapper.Map<PermisoGenericoDto>(permisoGenerico);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PermisoGenericoDto>> Post(PermisoGenericoDto permisoGenericoDto)
        {
            var permisoGenerico = _mapper.Map<PermisoGenerico>(permisoGenericoDto);
            if(permisoGenericoDto.FechaCreacion == DateTime.MinValue)
            {
                permisoGenericoDto.FechaCreacion = DateTime.Now;
                permisoGenerico.FechaCreacion = DateTime.Now;
            }
            if(permisoGenericoDto.FechaModificacion == DateTime.MinValue)
            {
                permisoGenericoDto.FechaModificacion = DateTime.Now;
                permisoGenerico.FechaModificacion = DateTime.Now;
            }
            this._unitOfWork.PermisosGenericos.Add(permisoGenerico);
            await _unitOfWork.SaveAsync();
            if(permisoGenerico == null)
            {
                return BadRequest();
            }
            permisoGenericoDto.Id = permisoGenerico.Id;
            return CreatedAtAction(nameof(Post), new {id = permisoGenericoDto.Id}, permisoGenericoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PermisoGenericoDto>> Put(int id, [FromBody] PermisoGenericoDto permisoGenericoDto)
        {
            if(permisoGenericoDto.Id == 0)
            {
                permisoGenericoDto.Id = id;
            }
            if(permisoGenericoDto == null)
            {
                return BadRequest();
            }
            permisoGenericoDto.Id = id;
            var permisoGenerico = _mapper.Map<PermisoGenerico>(permisoGenericoDto);
            _unitOfWork.PermisosGenericos.Update(permisoGenerico);
            await _unitOfWork.SaveAsync();
            return permisoGenericoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(int id)
        {
            var permisoGenerico = await _unitOfWork.PermisosGenericos.GetByIdAsync(id);
            if(permisoGenerico == null){
                return NotFound();
            }
            _unitOfWork.PermisosGenericos.Remove(permisoGenerico);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}