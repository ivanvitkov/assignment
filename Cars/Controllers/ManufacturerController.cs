using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cars.DTO;
using Cars.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Mapper;
using Cars.Models;
using Cars.Helpers;
using Newtonsoft.Json;
using LightQuery;
using LightQuery.Client;

namespace Cars.Controllers
{
    [Route("api/manufacturers")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private IRepositoryWrapper _repository;


        public ManufacturerController(IRepositoryWrapper repository)
        {
            _repository = repository;

        }
        [HttpGet]
        
        public IActionResult GetManufacturers([FromQuery] ManufacturersParameters parameters)
        {
            try
            {
              
                var manufacturers = _repository.Manufacturer.GetManufacturers(parameters);

                var metadata = new
                {
                    manufacturers.TotalCount,
                    manufacturers.PageSize,
                    manufacturers.CurrentPage,
                    manufacturers.TotalPages,
                    manufacturers.HasNext,
                    manufacturers.HasPrevious

                };
                Response.Headers.Add("X-Pagination:", JsonConvert.SerializeObject(metadata));
                


                var result = Mapping.Mapper.Map<IEnumerable<ManufacturerDTO>>(manufacturers);
                
                Log.Information("Returned all manufacturers from database");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error($"Something went wrong: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetById(int id)
        {
            try
            {
                var manufacturer = _repository.Manufacturer.GetById(id);
                var result = Mapping.Mapper.Map<ManufacturerDTO>(manufacturer);
                Log.Information("Returned manufacturer with Id: " + id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error($"Something went wrong: {ex.Message}");
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}/models")]
        public IActionResult GetCarModels(int id)
        {
            try
            {
                var manufacturer = _repository.Manufacturer.GetModels(id);
                var result = Mapping.Mapper.Map<ManufacturerDTO>(manufacturer);
                Log.Information("Returned all manufacturers with models from database");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error($"Something went wrong: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] CreateManufacturerDTO manufacturer)
        {
            try
            {
                if (manufacturer == null)
                {
                    Log.Error("Manufacturer object is null");
                    return BadRequest("Manufacturer is null");
                }
                if (!ModelState.IsValid)
                {
                    Log.Error("Invalid object sent from client");
                    return BadRequest("Object validation failed");
                }

                var manufacturerEntity = Mapping.Mapper.Map<Manufacturer>(manufacturer);

                _repository.Manufacturer.CreateManufacturer(manufacturerEntity);
                _repository.Save();

                var created = Mapping.Mapper.Map<ManufacturerDTO>(manufacturerEntity);
                return Ok("New manufacturer created");

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateManufacturer(int id, [FromBody] UpdateManufacturerDTO manufacturer)
        {
            try
            {


                if (manufacturer == null)
                {
                    return BadRequest("Object is null");
                }
                var manufacturerEntity = _repository.Manufacturer.GetById(id);
                if (manufacturerEntity == null)
                {
                    Log.Error("Manufacturer with Id: " + id + " not found");
                    return NotFound();
                }
                Mapping.Mapper.Map(manufacturer, manufacturerEntity);
                _repository.Manufacturer.UpdateManufacturer(manufacturerEntity);
                _repository.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                Log.Error("Server error: " + ex.Message);
                return StatusCode(500, "Server Error");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteManufacturer(int id)
        {
            try
            {
                var manufacturer = _repository.Manufacturer.GetById(id);
                if(manufacturer == null)
                {
                    return NotFound();
                }
                if (_repository.CarModel.GetCarModels(id).Any())
                {
                    return BadRequest("Cannot delete manufacturer. It has related car models. Delete those frist");
                }
                _repository.Manufacturer.DeleteManufacturer(manufacturer);
                _repository.Save();
                return Ok("Manufacturer with Id: " + id + " deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
