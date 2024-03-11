using Ticket.Manager.Application.Common;
using Ticket.Manager.Domain.Common;

namespace Ticket.Manager.Application.Places.CreatePlace;

public record CreatePlaceCommand(string Name, 
    string Street, string Number, string City, 
    int NoNumericPlaceCount, int SectorCount, int RowsCount, int SeatsInRowCount) : ICommand<Result>;