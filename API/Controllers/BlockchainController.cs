using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BlockchainController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public BlockchainController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<BlockchainDto>>> GetAllAsync()
    {
        var blockchains = await _unitOfWork.Blockchains.GetAllAsync();
        return _mapper.Map<List<BlockchainDto>>(blockchains);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlockchainDto>> GetAllAsync(int id)
    {
        var blockchain = await _unitOfWork.Blockchains.GetByIdAsync(id);
        if(blockchain == null)
        {
            return BadRequest();
        }
        return _mapper.Map<BlockchainDto>(blockchain);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BlockchainDto>> Post(BlockchainDto blockchainDto)
    {
        var blockchain = _mapper.Map<Blockchain>(blockchainDto);
        if(blockchainDto.FechaCreacion == DateTime.MinValue )
        {
            blockchainDto.FechaCreacion = DateTime.Now;
            blockchain.FechaCreacion = DateTime.Now;
        }
        if(blockchainDto.FechaModificacion == DateTime.MinValue)
        {
            blockchainDto.FechaModificacion = DateTime.Now;
            blockchain.FechaModificacion = DateTime.Now;
        }
        this._unitOfWork.Blockchains.Add(blockchain);
        await _unitOfWork.SaveAsync();
        if(blockchain == null)
        {
            return BadRequest();
        }
        blockchainDto.Id = blockchain.Id;
        return CreatedAtAction(nameof(Post), new {id = blockchainDto.Id}, blockchainDto);
    }

    [HttpPut("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlockchainDto>> Put(int id, [FromBody] BlockchainDto blockchainDto)
    {   
        if(blockchainDto.Id == 0)
        {
            blockchainDto.Id = id;
        }
        if(blockchainDto == null)
        {
            return BadRequest();
        }
        blockchainDto.Id = id;
        var blockchain = _mapper.Map<Blockchain>(blockchainDto);
        _unitOfWork.Blockchains.Update(blockchain);
        await _unitOfWork.SaveAsync();
        return blockchainDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var blockchain = await _unitOfWork.Blockchains.GetByIdAsync(id);
        if(blockchain == null)
        {
            return NotFound();
        }
        _unitOfWork.Blockchains.Remove(blockchain);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


}
