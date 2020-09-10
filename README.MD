# Scripting
```csharp
      Scripting.Script script = new Scripting.Script();
      var value = script.EvalAsync<int>("return 325 + 5;").Result;
      Assert.AreEqual(330, value);

      var value2 = script.EvalAsync<int>("255 + 3").Result;
      Assert.AreEqual(258, value2);

      script.Run("Variables[\"variable1\"] = 13;");
      script.Run("Variables[\"variable1\"] = 14;");
      script.Run("Variables[\"variable1\"] = \"Hello world\";");
      script.Run("Variables[\"variable2\"] = 13;");
      script.Run("Variables[\"variable3\"] = 123.123;");
      script.Run("Variables[\"variable4\"] = \"Hello world\";");

      Assert.ThrowsException<AggregateException>(() =>
      {
          var obj = script.Eval<string>("return Variables[\"variable1\"];");
      });
      var objx = script.Eval<string>("return ConvertOrDefault<string>(Variables[\"variable1\"]);");
      Assert.AreEqual("Hello world", objx);
      var objy = script.Eval<int>("ConvertOrDefault<int>(Variables[\"variable1\"]);");
      Assert.AreEqual(0, objy);
      var obj2 = script.Eval("Variables[\"variable1\"]");
      Assert.AreEqual("Hello world", obj2?.ToString());
      var obj3 = script.GetVariable<string>("variable1");
      Assert.AreEqual("Hello world", obj3);

      script.SetVariable("pepa", 325);
      script.SetVariable("pepa", 327);
      Assert.AreEqual(327, script.GetVariable("pepa"));
      script.SetVariable("pepa", "xx");
      Assert.AreEqual("xx", script.GetVariable("pepa"));
```