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
    public class RolVsMaestroController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RolVsMaestroController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RolVsMaestroDto>>>GetAllAsync()
        {
            var rolVsMaestros = await _unitOfWork.RolesVsMaestros.GetAllAsync();
            return _mapper.Map<List<RolVsMaestroDto>>(rolVsMaestros);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RolVsMaestroDto>>GetAllAsync(int id)
        {
            var rolVsMaestro = await _unitOfWork.RolesVsMaestros.GetByIdAsync(id);
            if(rolVsMaestro == null)
            {
                return BadRequest();
            }
            return _mapper.Map<RolVsMaestroDto>(rolVsMaestro);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RolVsMaestroDto>> Post(RolVsMaestroDto rolVsMaestroDto)
        {
            var rolVsMaestro = _mapper.Map<RolVsMaestro>(rolVsMaestroDto);
            if(rolVsMaestroDto.FechaCreacion == DateTime.MinValue)
            {
                rolVsMaestroDto.FechaCreacion = DateTime.Now;
                rolVsMaestro.FechaCreacion = DateTime.Now;
            }
            if(rolVsMaestroDto.FechaModificacion == DateTime.MinValue)
            {
                rolVsMaestroDto.FechaModificacion = DateTime.Now;
                rolVsMaestro.FechaModificacion = DateTime.Now;
            }
            this._unitOfWork.RolesVsMaestros.Add(rolVsMaestro);
            await _unitOfWork.SaveAsync();
            if(rolVsMaestro == null)
            {
                return BadRequest();
            }
            rolVsMaestroDto.Id = rolVsMaestro.Id;
            return CreatedAtAction(nameof(Post), new {id = rolVsMaestroDto.Id}, rolVsMaestroDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RolVsMaestroDto>> Put(int id, [FromBody] RolVsMaestroDto rolVsMaestroDto)
        {
            if(rolVsMaestroDto.Id == 0)
            {
                rolVsMaestroDto.Id = id;
            }
            if(rolVsMaestroDto == null)
            {
                return BadRequest();
            }
            rolVsMaestroDto.Id = id;
            var rolVsMaestro = _mapper.Map<RolVsMaestro>(rolVsMaestroDto);
            _unitOfWork.RolesVsMaestros.Update(rolVsMaestro);
            await _unitOfWork.SaveAsync();
            return rolVsMaestroDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(int id)
        {
            var rolVsMaestro = await _unitOfWork.RolesVsMaestros.GetByIdAsync(id);
            if(rolVsMaestro == null){
                return NotFound();
            }
            _unitOfWork.RolesVsMaestros.Remove(rolVsMaestro);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}