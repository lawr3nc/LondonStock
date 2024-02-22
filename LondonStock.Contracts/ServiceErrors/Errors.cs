using ErrorOr;

namespace LondonStock.Contracts.ServiceErrors;

public static class Errors{
    public static class Stock{
        public static Error InvalidTicker => Error.Validation(
            code: "Stock.InvalidName",
            description: $"Ticker must be atleast {Models.Stock.MinTickerLength} characters long and at most {Models.Stock.MaxTickerLength}"
        );
    }

    public static class Trade{
        public static Error InvalidShare => Error.Validation(
            code: "Trade.InvalidShare",
            description: "The number of shares bought or sold cannot be less than or equal to 0"
        );

        public static Error UnauthorisedBroker => Error.Unauthorized(
            code: "Trade.UnAuthorisedBroker",
            description: "The broker ID is not from an authorised broker"
        );
    }
}