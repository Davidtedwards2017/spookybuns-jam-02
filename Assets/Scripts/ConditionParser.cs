using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public static class ConditionParser
{
    public static bool ProcessConditionalStatement(string conditionalStatement)
    {
        if (string.IsNullOrEmpty(conditionalStatement))
        {
            return true;
        }

        bool result = true;
        var statements = SplitAndStatements(conditionalStatement);
        foreach (var statement in statements)
        {
            if (!EvaulateSimpleExpression(statement))
            {
                result = false;
                break;
            }
        }

        return result;
    }

    private static string[] SplitAndStatements(string statementText)
    {
        return Regex.Split(statementText, @"(&&)")
            .Where(e => !e.Equals("&&"))
            .ToArray();
    }

    private static bool EvaulateSimpleExpression(string expression)
    {
        var split = Regex.Split(expression, @"([=><])");
        string symbol = split[1];
        int conditionalValue = int.Parse(split[2]);
        int value = StatsController.Instance.GetFlagValue(split[0]);

        switch (symbol)
        {
            case "=":
                return value == conditionalValue;
            case ">":
                return value > conditionalValue;
            case "<":
                return value < conditionalValue;
        }

        return false;
    }
}
