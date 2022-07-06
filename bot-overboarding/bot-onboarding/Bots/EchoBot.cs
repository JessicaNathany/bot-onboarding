using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace bot_onboarding.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var messageRecebida = $"Bot Onboarding: {turnContext.Activity.Text}";
            
            if (InfoUsuario.Nome == null)
            {
                InfoUsuario.Nome = turnContext.Activity.Text;
                InfoUsuario.Mensagem = $"Olá, {InfoUsuario.Nome}! Qual seu time?";
                
                await turnContext.SendActivityAsync(MessageFactory.Text(InfoUsuario.Mensagem, InfoUsuario.Mensagem), cancellationToken);
                return;
            }

            if (InfoUsuario.Time == null && InfoUsuario.Nome != null)
            {
                InfoUsuario.Time = turnContext.Activity.Text;
                InfoUsuario.Mensagem = $"Legal, você vai amar o time {InfoUsuario.Time}!";

                await turnContext.SendActivityAsync(MessageFactory.Text(InfoUsuario.Mensagem, InfoUsuario.Mensagem), cancellationToken);

                var selecioneOpcoesTime = "Digite o número da sua função no time: \r\n  1- Desenvolvedor \r\n 2- UX \r\n 3- QA";
                await turnContext.SendActivityAsync(MessageFactory.Text(selecioneOpcoesTime, selecioneOpcoesTime), cancellationToken);
                return;
            }

            if(InfoUsuario.TipoPapel == 0)
            {
                var selecioneOpcoesDev = "Essas são os acessos que você precisa. Digite uma opção que deseja ter acesso:" +
                                          "\r\n 1- AWS \r\n 2- Gitlab \r\n 3- Banco de Dados \r\n 4- VPN";

                InfoUsuario.TipoPapel = (TipoPapel)Enum.Parse(typeof(TipoPapel), turnContext.Activity.Text, true);

                await turnContext.SendActivityAsync(MessageFactory.Text(selecioneOpcoesDev, selecioneOpcoesDev), cancellationToken);
                return;
            }

            if (InfoUsuario.Acessos == 0)
            {
                var selecioneOpcoesDev = "Essas são os acessos que você precisa. Digite uma opção que deseja ter acesso:" +
                                          "\r\n 1- AWS \r\n 2- Gitlab \r\n 3- Banco de Dados \r\n 4- VPN";

                InfoUsuario.TipoPapel = (TipoPapel)Enum.Parse(typeof(TipoPapel), turnContext.Activity.Text, true);

                await turnContext.SendActivityAsync(MessageFactory.Text(selecioneOpcoesDev, selecioneOpcoesDev), cancellationToken);
                return;
            }

            //for (int i = 0; i < 3; i++)
            //{
            //    var papel = string.Empty;

            //    switch(i)
            //    {
            //        case 0:
            //            papel = " 1- Desenvolvedor";
            //            break;

            //        case 1:
            //            papel = "2- UX";
            //            break;

            //        case 2:
            //            papel = "3- QA";
            //            break;
            //    }

            //    await turnContext.SendActivityAsync(MessageFactory.Text(papel, InformacaoUsuario.mensagem), cancellationToken);
            //}


        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Olá seja bem-vindo! Qual seu nome?";


            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
