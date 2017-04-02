/*
    ajax snippet

    arg0 : url : {0}
    arg1 : data : {1}
    arg2 : target id : {2}
*/

$.ajax({
    error: function (jqXHR, textStatus, errorThrown) {
        alert('whoops! [' + textStatus + '][' + errorThrown + '][' + '{0}' + ']');
    },
    dataType: "html",
    cache: false,
    type: 'get',
    url: '{0}',
    data: {1},
    async: false,
    success: function (data) {
        $('#{2}').html(data);
    }
}); //end ajax