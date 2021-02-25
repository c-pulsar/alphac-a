function buildFormFromSchema(createUri, schema) {
  $("form").jsonForm({
    schema: schema,
    form: [
      "*",
      {
        "type": "submit",
        "title": "Submit",
        "htmlClass": "btn btn-success"
      },
      {
        "type": "button",
        "title": "Cancel",
        "htmlClass": "btn btn-primary",
        "onClick": function (e) { window.history.back(); }
      }
    ],
    onSubmitValid: function (obj) {

      var xhr = new XMLHttpRequest();
      xhr.open("POST", createUri);
      xhr.setRequestHeader("content-type", "application/json;charset=UTF-8");

      xhr.onload = function () {
        if (xhr.status === 201) {
          window.location.replace(xhr.getResponseHeader("location"));
        }
        else {
          $("#res").html(`Server Error: ${xhr.status} ${xhr.response}`);
        }
      }

      xhr.onerror = function () {
        $("#res").html("Network connection error");
      };

      xhr.send(JSON.stringify(obj));
    }
  });
}