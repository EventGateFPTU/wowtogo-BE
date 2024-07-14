using Ardalis.Result;
using Domain.Responses.Responses_TicketType;
using MediatR;

namespace UseCases.UC_TicketType.Commands.CreateTicketType;
public record CreateTicketTypeCommand(Guid EventId,
                                        string Name,
                                        string Description,
                                        string ImageUrl,
                                        decimal Price,
                                        DateTimeOffset FromDate,
                                        DateTimeOffset ToDate,
                                        int Amount,
                                        int LeastAmountBuy,
                                        int MostAmountBuy
                                        ) : IRequest<Result<CreateTicketTypeResponse>>;