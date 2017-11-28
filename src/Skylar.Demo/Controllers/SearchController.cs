using System;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;

namespace Skylar
{
    public class SearchController : ApiController
    {
        #region Actions

        [HttpGet]
        [Route("api/search/{query}")]
        public string Get([FromUri] string query)
        {
            var sentences = SentenceParser.Parse(new Parser(query));
            var queryDefinition = EvisionLanguageProcessor.Processor.Parse(sentences.First());

            return JsonConvert.SerializeObject(CertificateRepository.Find(QueryDefinitionToInfoConverter.Convert(queryDefinition)));
        }

        #endregion
    }
}