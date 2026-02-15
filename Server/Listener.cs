using Shared.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Listener<SessionT> where SessionT : Session
    {
        private Socket _listenSocket;
        private Func<SessionT> _sessionFactory;
        private Action<SessionT> _onAcceptAsync;

        private bool _isListening = false;

        public Listener(IPEndPoint endPoint, Func<SessionT> sessionFactory, Action<SessionT> onAcceptAsync)
        {
            //문지기 생성
            _listenSocket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _sessionFactory = sessionFactory;
            _onAcceptAsync = onAcceptAsync;

            //문지기 교육
            _listenSocket.Bind(endPoint);
        }

        public void StartListen()
        {
            if(_isListening) return;

            //영업 시작
            //backlog 매개변수 : 최대 대기 수
            _listenSocket.Listen(10);

            SocketAsyncEventArgs args = new SocketAsyncEventArgs();   //한번 만들면 계속 재활용 가능
            args.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
            RegisterAccept(args);

            _isListening = true;
        }

        //요청
        void RegisterAccept(SocketAsyncEventArgs args)
        {
            args.AcceptSocket = null;

            bool pending = _listenSocket.AcceptAsync(args);
            if (pending == false)  //펜딩 없이 바로 연결 성공했을 시
            {
                OnAcceptCompleted(null, args);
            }
        }

        void OnAcceptCompleted(object sender, SocketAsyncEventArgs args)
        {
            if (args.SocketError == SocketError.Success)
            {
                //받는다
                SessionT session = _sessionFactory.Invoke();
                session.Start(args.AcceptSocket);
                session.OnConnected(args.AcceptSocket.RemoteEndPoint);

                _onAcceptAsync?.Invoke(session);
            }
            else
            {
                Console.WriteLine(args.SocketError.ToString());
            }

            RegisterAccept(args);
        }

        public Socket Accept()
        {
            return _listenSocket.Accept();
        }
    }
}
