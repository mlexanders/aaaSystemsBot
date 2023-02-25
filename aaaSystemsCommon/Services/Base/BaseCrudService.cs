using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Utils;
using System.Net;
using System.Text;

namespace aaaSystemsCommon.Services.Base
{
    public class BaseCRUDService<TEntity, TKey> : BaseService, ICrud<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        public BaseCRUDService(string backRoot, HttpClient httpClient, string entityRoot = null!) : base(entityRoot ?? typeof(TEntity).GetRoot(), backRoot, httpClient) { }

        public virtual async Task<TEntity> Get(TKey key)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(Root + "/" + key);
            return await Deserialize<TEntity>(httpResponse);
        }

        public virtual async Task<List<TEntity>> Get()
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(Root);
            return await Deserialize<List<TEntity>>(httpResponse);
        }

        public virtual async Task Post(TEntity item)
        {
            var json = Serialize(item);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root, data);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }

        public virtual async Task Post(List<TEntity> item)
        {
            var json = Serialize(item);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(Root.ToString() + "/createAll", data);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }

        public virtual async Task Patch(TEntity item)
        {
            TEntity entity = await Get(item.Id);
            if (entity == null) throw new ErrorResponseException(HttpStatusCode.NotFound, "Entity not found");
            var json = new StringContent(Serialize(item), Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PatchAsync(Root + "/" + item.Id, json);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }

        public virtual async Task Delete(TKey key)
        {
            HttpResponseMessage httpResponse = await httpClient.DeleteAsync(Root + "/" + key);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
