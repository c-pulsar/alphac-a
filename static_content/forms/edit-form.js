function buildFormFromSchema(postFormUri, schema) {
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
      },
      {
        "type": "button",
        "title": "Delete",
        "htmlClass": "visible btn btn-danger",
        "onClick": function (e) { onDelete(e); }
      }
    ],

    onSubmitValid: function (obj) {

      var xhr = new XMLHttpRequest();
      xhr.open("POST", postFormUri);
      xhr.setRequestHeader("content-type", "application/json;charset=UTF-8");

      xhr.onload = function () {
        if (xhr.status === 200) {
          window.location.replace(postFormUri);
        }
        else {
          $("#res").html(`Unexpected response status: ${xhr.status} ${xhr.response}`);
        }
      }

      xhr.onerror = function () {
        $("#res").html("Network connection error");
      };

      xhr.send(JSON.stringify(obj));
    }
  });

  function onDelete(e) {
    if (confirm("Are you sure?")) {

      var xhr = new XMLHttpRequest();
      xhr.open("DELETE", postFormUri);

      xhr.onload = function () {
        if (xhr.status === 200) {
          //window.location.replace(collectionUri);
        }
        else {
          $("#res").html(`Unexpected status code: ${xhr.status} ${xhr.response}`);
        }
      }

      xhr.onerror = function () {
        $("#res").html("Deleted Network connection error");
      };

      xhr.send(null);
    }
  }
}