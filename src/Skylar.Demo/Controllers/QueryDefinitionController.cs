using System;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;

namespace Skylar
{
    public class QueryDefinitionController : ApiController
    {
        #region Actions

        [HttpGet]
        [Route("api/querydefinition/{query}")]
        public string Get([FromUri] string query)
        {
            var sentences = SentenceParser.Parse(new Parser(query));
            var queryDefinition = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            return JsonConvert.SerializeObject(QueryDefinitionToInfoConverter.Convert(queryDefinition));
        }

        #endregion
    }
}