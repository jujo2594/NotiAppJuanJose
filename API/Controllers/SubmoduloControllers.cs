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
    public class SubmoduloControllers : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubmoduloControllers(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SubmoduloDto>>>GetAllAsync()
        {
            var submodulos = await _unitOfWork.Submodulos.GetAllAsync();
            return _mapper.Map<List<SubmoduloDto>>(submodulos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubmoduloDto>>GetAllAsync(int id)
        {
            var submodulo = await _unitOfWork.Submodulos.GetByIdAsync(id);
            if(submodulo == null)
            {
                return BadRequest();
            }
            return _mapper.Map<SubmoduloDto>(submodulo);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubmoduloDto>> Post(SubmoduloDto submoduloDto)
        {
            var submodulo = _mapper.Map<Submodulo>(submoduloDto);
            if(submoduloDto.FechaCreacion == DateTime.MinValue)
            {
                submoduloDto.FechaCreacion = DateTime.Now;
                submodulo.FechaCreacion = DateTime.Now;
            }
            if(submoduloDto.FechaModificacion == DateTime.MinValue)
            {
                submoduloDto.FechaModificacion = DateTime.Now;
                submodulo.FechaModificacion = DateTime.Now;
            }
            this._unitOfWork.Submodulos.Add(submodulo);
            await _unitOfWork.SaveAsync();
            if(submodulo == null)
            {
                return BadRequest();
            }
            submoduloDto.Id = submodulo.Id;
            return CreatedAtAction(nameof(Post), new {id = submoduloDto.Id}, submoduloDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubmoduloDto>> Put(int id, [FromBody] SubmoduloDto submoduloDto)
        {
            if(submoduloDto.Id == 0)
            {
                submoduloDto.Id = id;
            }
            if(submoduloDto == null)
            {
                return BadRequest();
            }
            submoduloDto.Id = id;
            var submodulo = _mapper.Map<Submodulo>(submoduloDto);
            _unitOfWork.Submodulos.Update(submodulo);
            await _unitOfWork.SaveAsync();
            return submoduloDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(int id)
        {
            var submodulo = await _unitOfWork.Submodulos.GetByIdAsync(id);
            if(submodulo == null){
                return NotFound();
            }
            _unitOfWork.Submodulos.Remove(submodulo);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}