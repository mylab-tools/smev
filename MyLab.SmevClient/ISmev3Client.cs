﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MyLab.SmevClient.Smev;

namespace MyLab.SmevClient
{
    public interface ISmev3Client: IDisposable
    {
        /// <summary>
        /// Отправка запроса
        /// </summary>
        /// <typeparam name="TServiceRequest"></typeparam>
        /// <param name="context">Параметры метода</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task<Smev3ClientResponse<SendRequestResponse>> SendRequestAsync<TServiceRequest>(
            SendRequestExecutionContext<TServiceRequest> context, CancellationToken cancellationToken = default)

            where TServiceRequest : new();

        /// <summary>
        /// Получение сообщения из очереди входящих ответов
        /// </summary>
        /// <param name="namespaceUri"></param>
        /// <param name="rootElementLocalName"></param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task<Smev3ClientResponse> GetResponseAsync(Uri namespaceUri, string rootElementLocalName, 
                                            CancellationToken cancellationToken = default);

        /// <summary>
        /// Получение сообщения из очереди входящих ответов c десереализацией ответа в тип T
        /// </summary>
        /// <typeparam name="TServiceResponse"></typeparam>
        /// <param name="namespaceUri"></param>
        /// <param name="rootElementLocalName"></param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task<Smev3ClientResponse<GetResponseResponse<TServiceResponse>>> GetResponseAsync<TServiceResponse>(
            Uri namespaceUri, string rootElementLocalName, CancellationToken cancellationToken = default)

            where TServiceResponse : new();

        /// <summary>
        /// Подтверждение получения ответа
        /// </summary>
        /// <param name="messageId">Ид. подтверждаемого сообщения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task<Smev3ClientResponse<AckResponse>> AckAsync(Guid messageId, bool accepted = true, CancellationToken cancellationToken = default);
    }
}