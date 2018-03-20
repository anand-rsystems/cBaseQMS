function fillTests() {
    var ddlCustomers = $("#ddlTest");
    ddlCustomers.empty().append('<option selected="selected" value="0" disabled = "disabled">Loading.....</option>');
    $.ajax({
        type: "GET",
        url: "/TestMaster/GetAllTest",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            ddlCustomers.empty().append('<option selected="selected" value="0">All Tests*</option>').append('<option  value="1">*All Unassigned Tests</option>');

            $.each(response, function () {
                ddlCustomers.append($("<option></option>").val(this['Value']).html(this['Text']));
            });
        },
        failure: function (response) {
            alert(response.data);
        },
        error: function (response) {
            alert(response.data);
        }
    });
}