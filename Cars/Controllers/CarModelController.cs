using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cars.DTO;
using Cars.Helpers;
using Cars.Models;
using Cars.Repository.Interfaces;
using Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Cars.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        private IRepositoryWrapper _repository;


        public CarModelController(IRepositoryWrapper repository)
        {
            _repository = repository;

        }

        [HttpGet]
        public IActionResult GetAllCarModels()
        {
            var models = _repository.CarModel.GetAllModels();
            if(models == null)
            {
                return NotFound("No data");
            }

            var res = Mapping.Mapper.Map<IEnumerable<CarModelDTO>>(models);
            return Ok(res);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetCarModels(int id)
        {
            var carModels = _repository.CarModel.GetCarModels(id);
            if(carModels == null)
            {
                return NotFound();
            }

            var result = Mapping.Mapper.Map<IEnumerable<CarModelDTO>>(carModels);
            return Ok(result);
           
            


        }
        [HttpGet("model/{id}", Name = "GetModel")]
        public IActionResult GetById(int id)
        {
            try
            {
                var model = _repository.CarModel.GetById(id);
                var result = Mapping.Mapper.Map<CarModelDTO>(model);
                Log.Information("Returned car model with Id: " + id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error($"Something went wrong: {ex.Message}");
                return BadRequest(ex);
            }
        }

        [HttpPost("{id}")]
        public IActionResult Post(int id,[FromBody] CarModelDTO model)
        {
            try
            {
                var manufacturer = _repository.Manufacturer.GetById(id);
                if (manufacturer ==null)
                {
                    {
                        Log.Error("Manufacturer not selected");
                        return BadRequest("Manufacturer does not exist");
                    }
                }
                if (model == null)
                {
                    Log.Error("Car model object is null");
                    return BadRequest("Car model is null");
                }
                if (!ModelState.IsValid)
                {
                    Log.Error("Invalid object sent from client");
                    return BadRequest("Object validation failed");
                }

                var modelEntity = Mapping.Mapper.Map<CarModel>(model);

                modelEntity.ManufacturerId = id;

                _repository.CarModel.CreateCarModel(modelEntity);
                _repository.Save();

                var created = Mapping.Mapper.Map<CarModelDTO>(modelEntity);
                return CreatedAtRoute("GetModel", new { id = created.Id }, created);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("model/{id}")]
        public IActionResult UpdateCarModel(int id, [FromBody] CarModelDTO model)
        {
            try
            {


                if (model == null)
                {
                    return BadRequest("Object is null");
                }
                var modelEntity = _repository.CarModel.GetById(id);
                if (modelEntity == null)
                {
                    Log.Error("Car model with Id: " + id + " not found");
                    return NotFound();
                }
                Mapping.Mapper.Map(model, modelEntity);
                _repository.CarModel.UpdateCarModel(modelEntity);
                _repository.Save();
                return Ok(modelEntity);
            }
            catch (Exception ex)
            {
                Log.Error("Server error: " + ex.Message);
                return StatusCode(500, "Server Error");
            }
        }

        [HttpDelete("model/{id}")]
        public IActionResult DeleteCarModel(int id)
        {
            try
            {
                var model = _repository.CarModel.GetById(id);
                if (model == null)
                {
                    return NotFound();
                }

                _repository.CarModel.DeleteCarModel(model);
                _repository.Save();
                return Ok("Car model with Id: " + id + " deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
       
    }
}
