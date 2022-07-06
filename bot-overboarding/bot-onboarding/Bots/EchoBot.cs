using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
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
            
            if (InformacaoUsuario.nome == null)
            {
                InformacaoUsuario.nome = turnContext.Activity.Text;
                InformacaoUsuario.mensagem = $"Olá, {InformacaoUsuario.nome}! Qual seu time?";
                
                await turnContext.SendActivityAsync(MessageFactory.Text(InformacaoUsuario.mensagem, InformacaoUsuario.mensagem), cancellationToken);
                return;
            }

            if (InformacaoUsuario.time == null && InformacaoUsuario.nome != null)
            {
                InformacaoUsuario.time = turnContext.Activity.Text;
                InformacaoUsuario.mensagem = $"Legal, você vai amar o time {InformacaoUsuario.time}!";

                await turnContext.SendActivityAsync(MessageFactory.Text(InformacaoUsuario.mensagem, InformacaoUsuario.mensagem), cancellationToken);

                var selecioneOpcoesTime = "Digite o número da sua função no time: \r\n  1- Desenvolvedor \r\n 2- UX \r\n 3- QA";
                await turnContext.SendActivityAsync(MessageFactory.Text(selecioneOpcoesTime, selecioneOpcoesTime), cancellationToken);
                return;

                if()

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

                
                
                return;
            }


           
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
