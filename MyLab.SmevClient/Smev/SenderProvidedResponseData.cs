﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using MyLab.SmevClient.Xml;

namespace MyLab.SmevClient.Smev
{
    public class SenderProvidedResponseData<T> :
        IXmlSerializable where T : new()
    {
        public string Id { get; set; }

        public Guid MessageID { get; set; }

        public RequestRejected[] RequestRejectionResons { get; set; }

        public RequestStatus Status { get; set; }

        public AsyncProcessingStatus ProcessingStatus { get; set; }

        /// <summary>
        /// Содержательная часть ответа, XML-документ.
        /// </summary>
        public MessagePrimaryContent<T> MessagePrimaryContent { get; set; }

        #region IXmlSerializable

        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            reader.ReadElementSubtreeContent(
                "SenderProvidedResponseData", Smev3NameSpaces.MessageExchangeTypes11, required: true,
                (respReader) =>
                {
                    respReader.ReadElementIfItCurrentOrRequired(
                        "MessageID", Smev3NameSpaces.MessageExchangeTypes11, required: true,
                        (r) => MessageID = Guid.Parse((string)r.ReadElementContentAsString()));

                    respReader.ReadElementIfItCurrentOrRequired(
                        "To", Smev3NameSpaces.MessageExchangeTypes11, required: true,
                        (r) => r.Skip());

                    respReader.ReadElementIfItCurrentOrRequired(
                        "MessagePrimaryContent", Smev3NameSpaces.MessageExchangeTypesBasic11, required: false,
                        (r) =>
                        {
                            var msgPrimaryContent = new MessagePrimaryContent<T>();

                            msgPrimaryContent.ReadXml(r);

                            MessagePrimaryContent = msgPrimaryContent;
                        });

                    respReader.ReadElementIfItCurrentOrRequired(
                        "PersonalSignature", Smev3NameSpaces.MessageExchangeTypes11, required: false,
                        (r) =>
                        {
                            r.Skip();
                        });

                    respReader.ReadElementIfItCurrentOrRequired(
                        "AttachmentHeaderList", Smev3NameSpaces.MessageExchangeTypes11, required: false,
                        (r) =>
                        {
                            r.Skip();
                        });

                    respReader.ReadElementIfItCurrentOrRequired(
                        "RefAttachmentHeaderList", Smev3NameSpaces.MessageExchangeTypes11, required: false,
                        (r) =>
                        {
                            r.Skip();
                        });

                    respReader.ReadElementIfItCurrentOrRequired(
                        "AsyncProcessingStatus", Smev3NameSpaces.MessageExchangeTypes11, required: false,
                        (r) =>
                        {
                            var status = new AsyncProcessingStatus();
                            status.ReadXml(r);
                            ProcessingStatus = status;
                        });
                    respReader.ReadElementIfItCurrentOrRequired("RequestRejected", Smev3NameSpaces.MessageExchangeTypes11, required: false, r =>
                    {
                        var requestRejectedList = new List<RequestRejected>();
                        while (reader.IsStartElement("RequestRejected", Smev3NameSpaces.MessageExchangeTypes11))
                        {
                            var requestRejected = new RequestRejected();
                            requestRejected.ReadXml(r);
                            requestRejectedList.Add(requestRejected);
                        }
                        RequestRejectionResons = requestRejectedList.ToArray();
                    });
                });
        }

        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
