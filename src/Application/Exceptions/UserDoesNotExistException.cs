namespace Application.Exceptions;

public class UserDoesNotExistException(
    string resourceName,
    params object[] resourceParams)
    : Exception($"User {resourceName} doesn't exist(Datbase). Params: {string.Join(',', resourceParams)}");