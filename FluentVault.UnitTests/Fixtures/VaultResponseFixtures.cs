using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoFixture;

namespace FluentVault.UnitTests.Fixtures;

internal partial class VaultResponseFixtures
{
    private static (string Body, IEnumerable<T> Files) CreateBody<T>(Fixture fixture, int count, string type, string typeNamespace, Func<T, string> createInnerBody)
    {
        List<T> entities = fixture.CreateMany<T>(count).ToList();
        StringBuilder bodybuilder = new();

        bodybuilder.Append(GetEnvelopeStart(type, typeNamespace));
        entities.ForEach(file => bodybuilder.Append(createInnerBody(file)));
        bodybuilder.Append(GetEnvelopeEnd(type));

        return (bodybuilder.ToString(), entities);
    }
    private static string GetEnvelopeStart(string type, string typeNamespace) => $@"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
<s:Body xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
    <{type}Response xmlns=""{typeNamespace}"">
            <{type}Result>";

    private static string GetEnvelopeEnd(string type) => $@"</{type}Result>
		</{type}Response>
    </s:Body>
</s:Envelope>";

    private static StringBuilder CreateEntityBody<T>(IEnumerable<T> entities, Func<T, string> createBody)
        => entities.Aggregate(new StringBuilder(), (builder, entity) => builder.Append(createBody(entity)));

    private static StringBuilder CreateNestedElementArray(string parentName, string childName, IEnumerable<string> values)
        => CreateElement(parentName, CreateElementArray(childName, values));

    private static StringBuilder CreateElementArray(string name, IEnumerable<string> values)
        => values.Aggregate(new StringBuilder(), (builder, value) => builder.Append(CreateElement(name, value)));

    private static StringBuilder CreateElement(string name, StringBuilder builder)
        => CreateElement(name, builder.ToString());

    private static StringBuilder CreateElement(string name, string value)
        => new StringBuilder()
            .Append('<')
            .Append(name)
            .Append('>')
            .Append(value)
            .Append("</")
            .Append(name)
            .Append('>');
}
