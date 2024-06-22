using Ardalis.Result;
using MediatR;

namespace UseCases.UC_TicketType.Commands.CreateTicketType;
public record CreateTicketTypeCommand(Guid showId,
                                        string Name,
                                        string Description,
                                        string ImageUrl,
                                        decimal Price,
                                        DateTimeOffset FromDate,
                                        DateTimeOffset ToDate,
                                        int Amount,
                                        int LeastAmountBuy,
                                        int MostAmountBuy
                                        ) : IRequest<Result>;