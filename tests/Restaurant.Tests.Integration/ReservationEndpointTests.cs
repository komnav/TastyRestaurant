using System.Net;
using System.Net.Http.Json;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Application.Dtos.Reservation.Requests;
using Application.Dtos.Reservation.Responses;

namespace Restaurant.Tests.Integration;

public class ReservationEndpointTests : BaseTest
{
    const string SuperAdmin = "SuperAdmin";
    const string Password = "Admin1234$";

    [Test]
    public async Task CreateReservationEndpointTest()
    {
        //Arrange
        var table = new Table
        {
            Number = 1,
            Capacity = 2,
            Type = TableType.Table
        };
        await LoginAsync(SuperAdmin, Password);
        await CreateEntity(table);

        var now = DateTimeOffset.UtcNow;
        var reservation =
            new CreateReservationRequestModel
            {
                TableId = table.Id,
                CustomerId = 1,
                From = new DateTimeOffset
                (
                    now.Year,
                    now.Month,
                    now.Day,
                    now.Hour,
                    now.Minute,
                    0,
                    now.Offset
                ),
                To = new DateTimeOffset
                (
                    now.Year,
                    now.Month,
                    now.Day,
                    now.Hour + 1,
                    now.Minute,
                    0,
                    now.Offset
                ),
                Notes = "string",
                Status = 0
            };
        //Act
        var response = await HttpClient.PostAsJsonAsync("Reservation", reservation);

        //Assert
        response.EnsureSuccessStatusCode();
        var getReservation = await GetEntity<Reservation>(r =>
            r.TableId == reservation.TableId &&
            r.UserId == reservation.CustomerId &&
            r.From == reservation.From &&
            r.To == reservation.To &&
            r.Notes == reservation.Notes &&
            r.Status == reservation.Status);
        getReservation.Should().NotBeNull();
    }

    [Test]
    public async Task CreateDuplicateReservationEndpointTest()
    {
        //Arrange
        var table = new Table
        {
            Number = 1,
            Capacity = 2,
            Type = TableType.Table
        };
        await LoginAsync(SuperAdmin, Password);
        await CreateEntity(table);

        var contact = new Contact
        {
            FirstName = "FirstName",
            LastName = "LastName"
        };

        var user = new User
        {
            UserName = "UserName",
            Contact = contact
        };
        await CreateUser(contact, user);

        var now = DateTimeOffset.UtcNow;
        var reservation = new Reservation
        {
            UserId = user.Id,
            TableId = table.Id,
            From = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            To = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            Notes = "Notes",
            Status = 0
        };
        await CreateEntity(reservation);

        var request =
            new CreateReservationRequestModel
            {
                TableId = reservation.TableId,
                CustomerId = reservation.UserId,
                From = reservation.From,
                To = reservation.To,
                Notes = reservation.Notes,
                Status = reservation.Status
            };
        //Act
        var response = await HttpClient.PostAsJsonAsync("Reservation", request);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task DeleteReservationEndpointTest()
    {
        //Arrange
        var table = new Table
        {
            Number = 1,
            Capacity = 2,
            Type = TableType.Table
        };
        await LoginAsync(SuperAdmin, Password);
        await CreateEntity(table);

        var contact = new Contact
        {
            FirstName = "FirstName",
            LastName = "LastName"
        };

        var user = new User
        {
            UserName = "UserName",
            Contact = contact
        };
        await CreateUser(contact, user);

        var now = DateTimeOffset.UtcNow;
        var reservation = new Reservation
        {
            UserId = user.Id,
            TableId = table.Id,
            From = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            To = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            Notes = "Notes",
            Status = 0
        };
        await CreateEntity(reservation);

        //Act
        var response = await HttpClient.DeleteAsync($"/Reservation/{reservation.Id}");

        //Assert
        response.EnsureSuccessStatusCode();
        var tryToFindReservation = await GetEntity<Reservation>(x => x.Id == reservation.Id);
        tryToFindReservation.Should().BeNull();
    }

    [Test]
    public async Task DeleteEmptyReservationEndpointTest()
    {
        //Arrange
        await LoginAsync(SuperAdmin, Password);

        //Act
        var response = await HttpClient.DeleteAsync($"/Reservation/1234");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task UpdateReservationEndpointTest()
    {
        //Arrange
        var firstTable = new Table
        {
            Number = 1,
            Capacity = 2,
            Type = TableType.Table
        };
        await LoginAsync(SuperAdmin, Password);
        await CreateEntity(firstTable);

        var secondTable = new Table
        {
            Number = 11,
            Capacity = 2,
            Type = TableType.Table
        };
        await CreateEntity(secondTable);

        var contact = new Contact
        {
            FirstName = "FirstName",
            LastName = "LastName"
        };

        var user = new User
        {
            UserName = "UserName",
            Contact = contact
        };
        await CreateUser(contact, user);

        var now = DateTimeOffset.UtcNow;
        var reservation = new Reservation
        {
            UserId = user.Id,
            TableId = firstTable.Id,
            From = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            To = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            Notes = "Notes",
            Status = 0
        };
        await CreateEntity(reservation);

        var updateReservation = new UpdateReservationRequestModel
        {
            TableId = secondTable.Id,
            CustomerId = 1,
            From = reservation.From,
            To = reservation.To,
            Notes = "stringUpdate",
            Status = ReservationStatus.Cancelled
        };

        //Act
        var response = await HttpClient.PutAsJsonAsync($"Reservation/{reservation.Id}", updateReservation);

        //Assert
        response.EnsureSuccessStatusCode();
        var tryToFindReservation = await GetEntity<Reservation>(x =>
            x.Id == reservation.Id &&
            x.TableId == updateReservation.TableId &&
            x.UserId == updateReservation.CustomerId &&
            x.From == updateReservation.From &&
            x.To == updateReservation.To &&
            x.Status == updateReservation.Status
        );
        tryToFindReservation.Should().NotBeNull();
    }

    [Test]
    public async Task UpdateDuplicateReservationEndpointTest()
    {
        //Arrange
        var firstTable = new Table
        {
            Number = 1,
            Capacity = 2,
            Type = TableType.Table
        };
        await LoginAsync(SuperAdmin, Password);
        await CreateEntity(firstTable);

        var contact = new Contact
        {
            FirstName = "FirstName",
            LastName = "LastName"
        };

        var user = new User
        {
            UserName = "UserName",
            Contact = contact
        };
        await CreateUser(contact, user);

        var now = DateTimeOffset.UtcNow.AddDays(2);
        var firstReservation = new Reservation
        {
            UserId = user.Id,
            TableId = firstTable.Id,
            From = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            To = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            Notes = "Notes",
            Status = 0
        };
        await CreateEntity(firstReservation);

        var now2 = DateTimeOffset.UtcNow;
        var secondReservation = new Reservation
        {
            UserId = user.Id,
            TableId = firstTable.Id,
            From = new DateTimeOffset
            (
                now2.Year,
                now2.Month,
                now2.Day,
                now2.Hour,
                now2.Minute,
                0,
                now2.Offset
            ),
            To = new DateTimeOffset
            (
                now2.Year,
                now2.Month,
                now2.Day,
                now2.Hour,
                now2.Minute,
                0,
                now2.Offset
            ),
            Notes = "Notes",
            Status = 0
        };
        await CreateEntity(secondReservation);

        var updateReservation = new UpdateReservationRequestModel
        {
            TableId = firstTable.Id,
            CustomerId = firstReservation.UserId,
            From = firstReservation.From,
            To = firstReservation.To,
            Notes = firstReservation.Notes,
            Status = firstReservation.Status
        };

        //Act
        var response = await HttpClient.PutAsJsonAsync($"Reservation/{secondReservation.Id}", updateReservation);

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task GetReservationByIdEndpointTest()
    {
        //Arrange
        var table = new Table
        {
            Number = 1,
            Capacity = 2,
            Type = TableType.Table
        };
        await CreateEntity(table);

        var contact = new Contact
        {
            FirstName = "FirstName",
            LastName = "LastName"
        };

        var user = new User
        {
            UserName = "UserName",
            Contact = contact
        };
        await CreateUser(contact, user);

        DateTimeOffset now = DateTimeOffset.UtcNow;
        var reservation = new Reservation
        {
            UserId = user.Id,
            TableId = table.Id,
            From = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            To = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            Notes = "Notes",
            Status = 0
        };
        await CreateEntity(reservation);

        //Act
        var response = await HttpClient.GetFromJsonAsync<GetReservationResponseModel>(
            $"Reservation/{reservation.Id}");

        //Assert
        response.Should().NotBeNull();
        response.TableId.Should().Be(reservation.TableId);
        response.CustomerId.Should().Be(reservation.UserId);
        response.From.Should().Be(reservation.From);
        response.To.Should().Be(reservation.To);
        response.Notes.Should().Be(reservation.Notes);
        response.Status.Should().Be(reservation.Status);
    }

    [Test]
    public async Task GetAllReservationEndpointTest()
    {
        //Arrange
        var table = new Table
        {
            Number = 1,
            Capacity = 2,
            Type = TableType.Table
        };
        await CreateEntity(table);

        var contact = new Contact
        {
            FirstName = "FirstName",
            LastName = "LastName"
        };

        var user = new User
        {
            UserName = "UserName",
            Contact = contact
        };
        await CreateUser(contact, user);

        DateTimeOffset now = DateTimeOffset.UtcNow;
        var firstReservation = new Reservation
        {
            UserId = user.Id,
            TableId = table.Id,
            From = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            To = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            Notes = "Notes",
            Status = 0
        };
        await CreateEntity(firstReservation);

        var secondReservation = new Reservation
        {
            UserId = user.Id,
            TableId = table.Id,
            From = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            To = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            Notes = "Notes",
            Status = 0
        };
        await CreateEntity(secondReservation);

        //Act
        var response = await HttpClient.GetFromJsonAsync<List<GetReservationResponseModel>>($"Reservation");

        //Assert
        response.Should().NotBeNull();
        response.Count.Should().Be(2);
    }

    [Test]
    public async Task CancelReservationEndpointTest()
    {
        //Arrange
        var table = new Table
        {
            Number = 1,
            Capacity = 2,
            Type = TableType.Table
        };
        await LoginAsync(SuperAdmin, Password);
        await CreateEntity(table);

        var contact = new Contact
        {
            FirstName = "FirstName",
            LastName = "LastName"
        };

        var user = new User
        {
            UserName = "UserName",
            Contact = contact
        };
        await CreateUser(contact, user);

        var now = DateTimeOffset.UtcNow;
        var reservation = new Reservation
        {
            UserId = user.Id,
            TableId = table.Id,
            From = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            To = new DateTimeOffset
            (
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                0,
                now.Offset
            ),
            Notes = "Notes",
            Status = 0
        };
        await CreateEntity(reservation);

        //Act
        var response = await HttpClient.PutAsJsonAsync($"/Reservation/cancel/{reservation.Id}", reservation);

        //Assert
        response.EnsureSuccessStatusCode();
        var tryToFindReservation = await GetEntity<Reservation>(x => x.Id == reservation.Id);
        tryToFindReservation!.Status.Should().Be(ReservationStatus.Cancelled);
    }
}