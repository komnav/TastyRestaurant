namespace Application.Exceptions;

public class ResourceAlreadyExistException(
    string resourceName,
    params object[] resourceParams)
    : Exception($"Resource {resourceName} was not created(Duplicate). Params: {string.Join(',', resourceParams)}");