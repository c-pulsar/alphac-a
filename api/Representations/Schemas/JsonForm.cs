using Newtonsoft.Json.Linq;

namespace AlphacA.Representations.Schemas
{
  public static class JsonForm
  {
    public static JArray Generate()
    {
      return new JArray(
        "*",
        // new JObject(
        //   new JProperty("type", "fieldset"),
        //   new JProperty("title", "Bala de coco"),
        //   new JProperty("expandable", true)
        // ),
        new JObject(
          new JProperty("type", "actions"),
          new JProperty(
            "items",
            new JArray(new JObject(
              new JProperty("type", "submit"),
              new JProperty("title", "Submit"))))));
    }
  }
}