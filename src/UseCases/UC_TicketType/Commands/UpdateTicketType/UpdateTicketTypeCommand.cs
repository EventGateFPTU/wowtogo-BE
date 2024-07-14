using Ardalis.Result;
using MediatR;

namespace UseCases.UC_TicketType.Commands.UpdateTicketType;
public record UpdateTicketTypeCommand(Guid Id,
                                    string Name,
                                    string Description,
                                    string ImageUrl,
                                    decimal Price,
                                    DateTimeOffset FromDate,
                                    DateTimeOffset ToDate,
                                    int Amount,
                                    int LeastAmountBuy,
                                    int MostAmountBuy) : IRequest<Result>;