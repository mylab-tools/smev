﻿using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using MyLab.SmevClient.Xml;

namespace MyLab.SmevClient.Smev
{
    public class Response<T> :
        IXmlSerializable where T : new()
    {
        public string Id { get; set; }

        public Guid? OriginalMessageId { get; set; }

        public string OriginalTransactionCode { get; set; }

        public Guid? ReferenceMessageID { get; set; }

        public SenderProvidedResponseData<T> SenderProvidedResponseData { get; set; }

        public MessageMetadata MessageMetadata { get; set; }

        #region IXmlSerializable

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadElementSubtreeContent(
                "Response", Smev3NameSpaces.MessageExchangeTypes11, required: true,
                (responseReader) =>
                {
                    responseReader.ReadElementIfItCurrentOrRequired(
                        "OriginalMessageId", Smev3NameSpaces.MessageExchangeTypes11, required: false,
                        (r) => OriginalMessageId = Guid.Parse((string)r.ReadElementContentAsString()));

                    responseReader.ReadElementIfItCurrentOrRequired(
                        "OriginalTransactionCode", Smev3NameSpaces.MessageExchangeTypes11, required: false,
                        (r) => OriginalTransactionCode = r.ReadElementContentAsString());

                    responseReader.ReadElementIfItCurrentOrRequired(
                        "ReferenceMessageID", Smev3NameSpaces.MessageExchangeTypes11, required: false,
                        (r) => ReferenceMessageID = Guid.Parse(r.ReadElementContentAsString()));

                    var senderProvidedResponseData = new SenderProvidedResponseData<T>();

                    senderProvidedResponseData.ReadXml(responseReader);

                    var messageMetadata = new MessageMetadata();

                    messageMetadata.ReadXml(responseReader);

                    MessageMetadata = messageMetadata;
                    SenderProvidedResponseData = senderProvidedResponseData;
                });
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
