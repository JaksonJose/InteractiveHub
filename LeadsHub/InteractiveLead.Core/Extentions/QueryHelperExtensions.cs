
using AdaptiveKitCore.Enums;
using AdaptiveKitCore.Model;
using AdaptiveKitCore.Requests;

namespace InteractiveLead.Core.Extentions
{
    public static class QueryHelperExtensions
    {
        public static string BuildWhereClause(this FilterRequest filterRequest)
        {
            string clausuleWhere = filterRequest.FilterDescriptors.Any() ? "WHERE " : string.Empty;

            bool isFirst = true;
            foreach (FilterDescriptor filter in filterRequest.FilterDescriptors)
            {
                bool hasAlias = !string.IsNullOrEmpty(filter.AliasName);

                string property = hasAlias ? $"{filter.AliasName}.\"{filter.PropertyName}\"" : $"\"{filter.PropertyName}\"";

                if (isFirst) 
                {
                    clausuleWhere += $"{property} {BuildComparisonStatement(filter)} ";                    
                }
                
                if (!isFirst)
                {
                    clausuleWhere += $"{filter.FilterConnector} {property} {BuildComparisonStatement(filter)} ";
                }

                isFirst = false;
            }

            return clausuleWhere;
        }

        private static string BuildComparisonStatement(FilterDescriptor filter)
        {
            string statement = filter.FilterOperator switch
            { 
                FilterOperatorEnum.EqualTo => $"= '{filter.Value}'",
                FilterOperatorEnum.NotEqualTo => $"<> '{filter.Value}'",
                FilterOperatorEnum.Contains => $"ILIKE '%{filter.Value}%'",
                FilterOperatorEnum.StartsWith => $"ILIKE '%{filter.Value}'",
                FilterOperatorEnum.EndsWith => $"ILIKE '{filter.Value}%'",
                FilterOperatorEnum.GreaterThen => $"> '{filter.Value}'",
                FilterOperatorEnum.LessThen => $"< '{filter.Value}'",
                FilterOperatorEnum.GreaterThanOrEqualTo => $">= '{filter.Value}'",
                FilterOperatorEnum.LessThanOrEqualTo => $"<= '{filter.Value}'",
                FilterOperatorEnum.In => $"IN ({filter.Value})",
                FilterOperatorEnum.NotIn => $"NOT IN ({filter.Value})",
                FilterOperatorEnum.IsNull => "IS NULL",
                FilterOperatorEnum.IsNotNull => "IS NOT NULL",                
                _ => $"= '{filter.Value}'"
            };

            return statement;
        }
    }
}
