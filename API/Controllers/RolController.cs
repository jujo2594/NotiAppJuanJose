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
    public class RolController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RolController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<RolDto>>>GetAllAsync()
        {
            var roles = await _unitOfWork.Roles.GetAllAsync();
            return _mapper.Map<List<RolDto>>(roles);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RolDto>>GetAllAsync(int id)
        {
            var rol = await _unitOfWork.Roles.GetByIdAsync(id);
            if(rol == null)
            {
                return BadRequest();
            }
            return _mapper.Map<RolDto>(rol);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RolDto>> Post(RolDto rolDto)
        {
            var rol = _mapper.Map<Rol>(rolDto);
            if(rolDto.FechaCreacion == DateTime.MinValue)
            {
                rolDto.FechaCreacion = DateTime.Now;
                rol.FechaCreacion = DateTime.Now;
            }
            if(rolDto.FechaModificacion == DateTime.MinValue)
            {
                rolDto.FechaModificacion = DateTime.Now;
                rol.FechaModificacion = DateTime.Now;
            }
            this._unitOfWork.Roles.Add(rol);
            await _unitOfWork.SaveAsync();
            if(rol == null)
            {
                return BadRequest();
            }
            rolDto.Id = rol.Id;
            return CreatedAtAction(nameof(Post), new {id = rolDto.Id}, rolDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RolDto>> Put(int id, [FromBody] RolDto rolDto)
        {
            if(rolDto.Id == 0)
            {
                rolDto.Id = id;
            }
            if(rolDto == null)
            {
                return BadRequest();
            }
            rolDto.Id = id;
            var rol = _mapper.Map<Rol>(rolDto);
            _unitOfWork.Roles.Update(rol);
            await _unitOfWork.SaveAsync();
            return rolDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(int id)
        {
            var rol = await _unitOfWork.Roles.GetByIdAsync(id);
            if(rol == null){
                return NotFound();
            }
            _unitOfWork.Roles.Remove(rol);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}