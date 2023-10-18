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
    public class FormatoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FormatoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<FormatoDto>>>GetAllAsync()
        {
            var formatos = await _unitOfWork.Formatos.GetAllAsync();
            return _mapper.Map<List<FormatoDto>>(formatos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FormatoDto>>GetAllAsync(int id)
        {
            var formato = await _unitOfWork.Formatos.GetByIdAsync(id);
            if(formato == null)
            {
                return BadRequest();
            }
            return _mapper.Map<FormatoDto>(formato);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FormatoDto>> Post(FormatoDto formatoDto)
        {
            var formato = _mapper.Map<Formato>(formatoDto);
            if(formatoDto.FechaCreacion == DateTime.MinValue)
            {
                formatoDto.FechaCreacion = DateTime.Now;
                formato.FechaCreacion = DateTime.Now;
            }
            if(formatoDto.FechaModificacion == DateTime.MinValue)
            {
                formatoDto.FechaModificacion = DateTime.Now;
                formato.FechaModificacion = DateTime.Now;
            }
            this._unitOfWork.Formatos.Add(formato);
            await _unitOfWork.SaveAsync();
            if(formato == null)
            {
                return BadRequest();
            }
            formatoDto.Id = formato.Id;
            return CreatedAtAction(nameof(Post), new {id = formatoDto.Id}, formatoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FormatoDto>> Put(int id, [FromBody] FormatoDto formatoDto)
        {
            if(formatoDto.Id == 0)
            {
                formatoDto.Id = id;
            }
            if(formatoDto == null)
            {
                return BadRequest();
            }
            formatoDto.Id = id;
            var formato = _mapper.Map<Formato>(formatoDto);
            _unitOfWork.Formatos.Update(formato);
            await _unitOfWork.SaveAsync();
            return formatoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var formato = await _unitOfWork.Formatos.GetByIdAsync(id);
            if(formato == null)
            {
                return NotFound();
            }
            _unitOfWork.Formatos.Remove(formato);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}