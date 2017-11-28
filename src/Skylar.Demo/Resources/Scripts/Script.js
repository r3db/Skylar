$(document).ready(function ()
{
	displayQueryDefinition();
	displayQueryResults();

	$('#query').focus();

	$("#query").keyup(function ()
	{
	    displayQueryDefinition();
	    displayQueryResults();
	});
	
	function displayQueryDefinition()
	{
	    $.ajax({
	        type: "GET",
	        url: "api/querydefinition/" + $('#query').val(),
	        error: function(xhr, statusText) 
	        {
	            $('#query_definition_result').html("Http Error: " + xhr.status);
	        },
	        success: function (data)
	        {
	            var qd = JSON.parse(data);

	            var result = '';
	            result += qd.Target + "<br/>";

	            for (var i = 0; i < qd.Properties.length; i++)
	            {
	                var item = qd.Properties[i];
	                result += item.Key.Content + " => " + item.Value.Content + "<br/>";
	            }

	            $('#query_definition_result').html(result);
	        }
	    });
	}

	function displayQueryResults()
	{
	    $.ajax({
	        type: "GET",
	        url: "api/search/" + $('#query').val(),
	        error: function(xhr, statusText)
	        {
	            $('#query_result').html('<pre><code>Http Error: ' + xhr.status + '</code></pre>');
	        },
	        success: function(data)
	        {
	            var queryResult = JSON.parse(data);
	            var result = '';

	            for (var i = 0; i < queryResult.length; i++)
	            {
	                var item = queryResult[i];
	                var keys = Object.keys(item);

	                var section = '';

	                if ((i % 2) == 0)
	                {
	                    section += '<div class="search-item">';
	                } else
	                {
	                    section += '<div class="search-item even">';
	                }

	                section += '    <article>';
	                section += '        <div class="article-content">';
	                section += '            <span class="numbering">' + (i + 1) + '</span>';
	                section += '            <div class="excerpt">';

	                for (var k = 0; k < Math.min(4, keys.length) ; k++)
	                {
	                    section += '                <p>' + keys[k] + " : " + item[keys[k]] + '</p>';
	                }

	                section += '            </div>';
	                section += '        </div>';
	                section += '    </article>';
	                section += '</div>';

	                result += section;
	            }

	            $('#query_result').html(result);
	        }
	    });
	}
});
