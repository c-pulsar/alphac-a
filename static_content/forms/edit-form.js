function buildFormFromSchema(postUri, deleteRedirectUri, schema) {

  var deleteButtonVisualAttributes = deleteRedirectUri
    ? "visible btn btn-danger"
    : "invisible";

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
        "htmlClass": deleteButtonVisualAttributes,
        "onClick": function (e) { onDelete(e); }
      }
    ],

    onSubmitValid: function (obj) {

      var xhr = new XMLHttpRequest();
      xhr.open("POST", postUri);
      xhr.setRequestHeader("content-type", "application/json;charset=UTF-8");

      xhr.onload = function () {
        if (xhr.status === 200) {
          window.location.replace(postUri);
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
      xhr.open("DELETE", postUri);

      xhr.onload = function () {
        if (xhr.status === 200) {
          window.location.replace(deleteRedirectUri);
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