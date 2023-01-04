﻿using aaaSystemsApi.Repository;
using aaaSystemsCommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : BaseCrudController<Participant>
    {
        public ParticipantController(BaseCrudRepository<Participant> repository) : base(repository) { }
    }
}
