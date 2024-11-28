// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    
    GetData();
})

// for gate data
function GetData() {

    $.ajax({
        url:'/Student/GetAll',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response == null || response == undefined)
            {

                var object = '';

                object = object + '<tr>';
                object = object + '<td>' + ' Data is not here' + '</td>';
                object = object + '</tr>';
                $('#Tblbody').html(object);
            }

            var object1 = '';
            $.each(response, function (index, item) {
                object1 = object1 + '<tr>';
                object1 = object1 + '<td>' + item.id + '</td>';
                object1 = object1 + '<td>' + item.name + '</td>';
                object1 = object1 + '<td>' + item.phone + '</td>';
                object1 = object1 + '<td>' + item.email + '</td>';
                object1 = object1 + '<td>' + item.password + '</td>';
                object1 = object1 + '<td>' + item.isAdmin + '</td>';
                object1 = object1 + '<td>' + item.categorys + '</td>';
                object1 = object1 + '<td> <button onclick="Edit(' + item.id + ')" > Edit</button></td>'
                object1 = object1 + '<td> <button onclick="Delete(' + item.id + ')" > Delete</button></td>'

            });
            $('#Tblbody').html(object1);
        },
        error: function () {

        },
    })
};

$('#ButtonAdd').click(function () {
    $('#Pmodel').modal('show');
});


//insert  record
function Insert() {
    var output = new Object();
    output.id = $('#id').val();
    output.name = $('#name').val();
    output.phone = $('#phone').val();
    output.email = $('#email').val();
    output.password = $('#password').val();
    output.isAdmin = $('#isAdmin').val();
    output.catId = $('#catid').val();
    $.ajax({
        url: 'Student/InsertData/',
        type: 'post',
        data: output,
        success: function (response) {
            if (response) {

                alert(response);
            }
        },
        error: function () {

            alert('faild');
        }
    })
}
// for delete
function Delete(id)
{
    if (confirm('do you want to delete record'))
    {
        $.ajax({
            url: 'Student/Delete?id='+id,
            dataType: 'json',
            type: 'post',   
            success: function (response) {
                    alert(response)
            },
            error: function () {
                alert('error')
            }
        })
    }
}

// for edit
function Edit(id)
{
    $.ajax({

        url: 'Student/GetById?id='+ id,
        type: 'post',
        dataType: 'json',
        success: function (response)
        {
            if (response == null || response == undefined) {

                alert('could not read the data')
            }
            $('#Pmodel').modal('show');
            $('#Mtitle').text('Edit record');
            $('#id').val(response.id);
            $('#name').val(response.name);
            $('#phone').val(response.phone);
            $('#email').val(response.email);
            $('#password').val(response.password);
            $('#isAdmin').val(response.isAdmin);
            $('#catid').val(response.catId);
            $('#save').text('Edit')
        },
        error: function ()
        {
            alert('error occured')
        }
    })
}


