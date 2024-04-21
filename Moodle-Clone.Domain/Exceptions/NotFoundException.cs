namespace MoodleClone.Domain.Exceptions;

public class NotFoundException(string resoruceType, string resourceIdentifier) : Exception($"{resoruceType} with id: {resourceIdentifier} doesn't exisit")
{

}
