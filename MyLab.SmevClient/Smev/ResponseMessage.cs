﻿using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using MyLab.SmevClient.Xml;

namespace MyLab.SmevClient.Smev
{
    /// <summary>
    /// Ответ, присланный поставщиком данных.
    /// </summary>
    public class ResponseMessage<T> : IXmlSerializable where T : new()
    {
        public Response<T> Response { get; set; }

        #region IXmlSerializable

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadElementSubtreeContent(
                "ResponseMessage", Smev3NameSpaces.MessageExchangeTypes11, required: true,
                (respReader) =>
                {
                    var response = new Response<T>();

                    response.ReadXml(respReader);

                    Response = response;

                    // AttachmentContentList
                    respReader.ReadElementIfItCurrentOrRequired(
                        "AttachmentContentList", Smev3NameSpaces.MessageExchangeTypes11, required: false,
                        (r) => r.Skip());

                    // SMEVSignature
                    respReader.ReadElementIfItCurrentOrRequired(
                        "SMEVSignature", Smev3NameSpaces.MessageExchangeTypes11, required: false,
                        (r) => r.Skip());
                });
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
