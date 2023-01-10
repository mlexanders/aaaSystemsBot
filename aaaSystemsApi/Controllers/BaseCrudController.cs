﻿using aaaSystemsApi.Repository;
using aaaSystemsCommon.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    public class BaseCrudController<TEntity> : ControllerBase, ICrud<TEntity, int> where TEntity : class, IEntity
    {
        protected readonly BaseCrudRepository<TEntity> repository;

        public BaseCrudController(BaseCrudRepository<TEntity> repository)
        {
            this.repository = repository;
        }


        [HttpPost]
        public virtual async Task Post(TEntity entity)
        {
            await repository.Create(entity);
        }


        [HttpPost("Many")]
        public virtual async Task Post(List<TEntity> entities)
        {
            await repository.Create(entities);
        }


        [HttpGet]
        public virtual Task<List<TEntity>> Get()
        {
            return repository.Read();
        }


        [HttpGet("{id}")]
        public virtual async Task<TEntity> Get(int id)
        {
            return await repository.ReadFirst(entity => entity.Id == id);
        }

        [HttpPatch]
        public virtual async Task Patch(TEntity entity)
        {
            await repository.Update(entity);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await repository.Delete(id);
        }
    }
}
