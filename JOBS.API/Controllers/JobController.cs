using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using JOBS.BLL.DTOs.Respponces;
using JOBS.BLL.Operations.Jobs.Commands;
using JOBS.BLL.Common.Validation;
using JOBS.BLL.Operations.Jobs.Queries;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace JOBS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private IMediator Mediator;
        private UpdateJobCommandValidator UdateJobCommandValidator;
        private CreateJobCommandValidator CreateJobCommandValidator;
        private readonly IDistributedCache distributedCache;

        public JobController(IMediator mediator, UpdateJobCommandValidator updateJobCommandValidator, CreateJobCommandValidator createJobCommandValidator, IDistributedCache distributedCache)
        {
            Mediator = mediator;
            UdateJobCommandValidator = updateJobCommandValidator;
            CreateJobCommandValidator = createJobCommandValidator;
            this.distributedCache = distributedCache;
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await Mediator.Send(new DeleteJobCommand { Id = id });
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateJobCommand comand)
        {
            try
            {
                var isValid = CreateJobCommandValidator.Validate(comand);
                if (isValid.IsValid)
                {
                    await Mediator.Send(comand);
                    return Ok();
                }
                else
                {
                    return ValidationProblem(isValid.Errors.ToString());
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }    
      //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Mechanic")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<JobDTO>>> GetAllAsync()
        {
            try
            {

                var cacheKey = "JobList";
                string serializedList;
                var List = new List<JobDTO>();
                var redisList = await distributedCache.GetAsync(cacheKey);
                if (redisList != null)
                {
                    serializedList = Encoding.UTF8.GetString(redisList);
                    List = JsonConvert.DeserializeObject<List<JobDTO>>(serializedList);

                }
                else
                {
                    List = (List<JobDTO>)await Mediator.Send(new GetJobsQuery());
                    serializedList = JsonConvert.SerializeObject(List);
                    redisList = Encoding.UTF8.GetBytes(serializedList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddSeconds(10))
                        .SetSlidingExpiration(TimeSpan.FromSeconds(1));
                    await distributedCache.SetAsync(cacheKey, redisList, options);
                }

                return Ok(List);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    //    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Mechanic")]
        [HttpGet("GetJobByMechanicId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<JobDTO>>> GetJobByMechanicIdAsync([FromQuery]Guid Id)
        {
            try
            {

  
                 var  List = (List<JobDTO>)await Mediator.Send(new GetJobsByMechanicIdQuery() {MecchanicId = Id });
    

                return Ok(List);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
      //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, Mechanic, User")]
        [HttpGet("GetJobsBYUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<JobDTO>>> GetJobsBYUserIdAsync(Guid Id)
        {
            try
            {

               
                  var List = (List<JobDTO>)await Mediator.Send(new GetJobsByUserIdQuery() {UserId = Id });
                

                return Ok(List);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
     //   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Mechanic,User")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JobDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var results = await Mediator.Send(new GetJobByIdQuery() { Id = id });


                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(UpdateJobCommand comand)
        {
            try
            {
                var isValid = UdateJobCommandValidator.Validate(comand);
                if (isValid.IsValid)
                {
                    await Mediator.Send(comand);
                    return Ok();
                }
                else
                {
                    return ValidationProblem(isValid.Errors.ToString());
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

    }
}
