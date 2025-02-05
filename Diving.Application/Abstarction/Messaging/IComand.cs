﻿using MediatR;

namespace Diving.Application.Abstarction.Messaging;

public interface ICommand : IRequest;

public interface ICommand<TResponse> : IRequest<TResponse>;
