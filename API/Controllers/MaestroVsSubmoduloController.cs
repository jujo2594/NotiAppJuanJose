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
    public class MaestroVsSubmoduloController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaestroVsSubmoduloController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MaestroVsSubmoduloDto>>>GetAllAsync()
        {
            var maestroVsSubmodulos = await _unitOfWork.MaestrosVsSubmodulos.GetAllAsync();
            return _mapper.Map<List<MaestroVsSubmoduloDto>>(maestroVsSubmodulos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MaestroVsSubmoduloDto>>GetAllAsync(int id)
        {
            var maestroVsSubmodulo = await _unitOfWork.MaestrosVsSubmodulos.GetByIdAsync(id);
            if(maestroVsSubmodulo == null)
            {
                return BadRequest();
            }
            return _mapper.Map<MaestroVsSubmoduloDto>(maestroVsSubmodulo);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MaestroVsSubmoduloDto>> Post(MaestroVsSubmoduloDto maestroVsSubmoduloDto)
        {
            var maestroVsSubmodulo = _mapper.Map<MaestroVsSubmodulo>(maestroVsSubmoduloDto);
            if(maestroVsSubmoduloDto.FechaCreacion == DateTime.MinValue)
            {
                maestroVsSubmoduloDto.FechaCreacion = DateTime.Now;
                maestroVsSubmodulo.FechaCreacion = DateTime.Now;
            }
            if(maestroVsSubmoduloDto.FechaModificacion == DateTime.MinValue)
            {
                maestroVsSubmoduloDto.FechaModificacion = DateTime.Now;
                maestroVsSubmodulo.FechaModificacion = DateTime.Now;
            }
            this._unitOfWork.MaestrosVsSubmodulos.Add(maestroVsSubmodulo);
            await _unitOfWork.SaveAsync();
            if(maestroVsSubmodulo == null)
            {
                return BadRequest();
            }
            maestroVsSubmoduloDto.Id = maestroVsSubmodulo.Id;
            return CreatedAtAction(nameof(Post), new {id = maestroVsSubmoduloDto.Id}, maestroVsSubmoduloDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MaestroVsSubmoduloDto>> Put(int id, [FromBody] MaestroVsSubmoduloDto maestroVsSubmoduloDto)
        {
            if(maestroVsSubmoduloDto.Id == 0)
            {
                maestroVsSubmoduloDto.Id = id;
            }
            if(maestroVsSubmoduloDto == null)
            {
                return BadRequest();
            }
            maestroVsSubmoduloDto.Id = id;
            var maestroVsSubmodulo = _mapper.Map<MaestroVsSubmodulo>(maestroVsSubmoduloDto);
            _unitOfWork.MaestrosVsSubmodulos.Update(maestroVsSubmodulo);
            await _unitOfWork.SaveAsync();
            return maestroVsSubmoduloDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>Delete(int id)
        {
            var maestroVsSubmodulo = await _unitOfWork.MaestrosVsSubmodulos.GetByIdAsync(id);
            if(maestroVsSubmodulo == null){
                return NotFound();
            }
            _unitOfWork.MaestrosVsSubmodulos.Remove(maestroVsSubmodulo);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}