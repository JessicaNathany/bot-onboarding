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
        //const string voltar;
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var messageRecebida = $"Bot Onboarding: {turnContext.Activity.Text}";
            var inputUsuario = turnContext.Activity.Text;

            var voltar = turnContext.Activity.Text;

            if (voltar != null && voltar.ToUpper() == "V")
            {
                InfoUsuario.Time = null;
                InfoUsuario.Funcao = null;
                InfoUsuario.Nome = null;
            }

            if (InfoUsuario.Nome == null && inputUsuario.Length > 1)
            {
                InfoUsuario.Nome = turnContext.Activity.Text;
                InfoUsuario.Mensagem = $"Olá, {InfoUsuario.Nome}! Qual seu time?";
                
                await turnContext.SendActivityAsync(MessageFactory.Text(InfoUsuario.Mensagem, InfoUsuario.Mensagem), cancellationToken);
                return;
            }
            else if(InfoUsuario.Nome == null && inputUsuario.Length <= 1 && !inputUsuario.Equals("v", StringComparison.InvariantCultureIgnoreCase))
            {
                InfoUsuario.Mensagem = $"Por gentileza, informe um nome válido.";

                await turnContext.SendActivityAsync(MessageFactory.Text(InfoUsuario.Mensagem, InfoUsuario.Mensagem), cancellationToken);
                return;
            }
            else
            {
                var welcomeText = "Olá seja bem-vindo! Qual seu nome?";
                await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
            }

            if (InfoUsuario.Time == null && InfoUsuario.Nome != null)
            {
                InfoUsuario.Time = turnContext.Activity.Text;
                InfoUsuario.Mensagem = $"Legal, você vai amar o time {InfoUsuario.Time}!";

                await turnContext.SendActivityAsync(MessageFactory.Text(InfoUsuario.Mensagem, InfoUsuario.Mensagem), cancellationToken);

                var selecioneOpcoesTime = "Digite o número da sua função no time: \r\n  1- Desenvolvedor \r\n 2- UX \r\n 3- QA \r\n 4- SM \r\n 5- PO";
                await turnContext.SendActivityAsync(MessageFactory.Text(selecioneOpcoesTime, selecioneOpcoesTime), cancellationToken);
                return;
            }

            if(InfoUsuario.TipoPapel == 0)
            {
                var selecioneOpcoesDev = "Essas são os acessos que você precisa. Digite uma opção que deseja ter acesso ou a letra V para voltar ao inicio:" +
                                          "\r\n 1- AWS \r\n 2- Gitlab \r\n 3- Banco de Dados \r\n 4- VPN \r\n 5- Jira ou Confluence \r\n 6- Grupo Teams \r\n 'V'- Voltar ao inicio";

                InfoUsuario.TipoPapel = (TipoPapel)Enum.Parse(typeof(TipoPapel), turnContext.Activity.Text, true);

                await turnContext.SendActivityAsync(MessageFactory.Text(selecioneOpcoesDev, selecioneOpcoesDev), cancellationToken);
                return;
            }

            if (InfoUsuario.TipoAcesso == 0)
            {
                var selecioneOpcoesAcesso = string.Empty;

                InfoUsuario.TipoAcesso = (Acessos)Enum.Parse(typeof(Acessos), turnContext.Activity.Text, true);

                if(InfoUsuario.TipoAcesso == Acessos.AWS)
                {
                    selecioneOpcoesAcesso  = "Para conseguir o acesso a AWS, siga caminho abaixo: \r\n" +
                                         " Acesse o (Acesso Globo) no seguinte link: https://acesso.g.globo/accessRequest/accessRequest.jsf#/accessRequest \r\n" +
                                         "1º Selecione a opção Solicitar/Remover Acessos \r\n" +
                                         "2º No campo de busca, digite AWS e selecione o perfil e o time no qual você pertence, exemplo: \r\n" +
                                         "AWS | JCORP - PRODUCAO ESTUDIOS | DEV - 874081334926 - GMUSIC \r\n" +
                                         "3º Após selecionar o perfil, clique em next e depois em Submit \r\n" +
                                         "Digite a letra V para voltar ao inicio";
                }

                if (InfoUsuario.TipoAcesso == Acessos.Gitlab)
                {
                    selecioneOpcoesAcesso = "Para conseguir o acesso a Gitlab, siga caminho abaixo: \r\n" +
                                         " Acesse o (Acesso Globo) no seguinte link: https://acesso.g.globo/accessRequest/accessRequest.jsf#/accessRequest \r\n" +
                                         "1º Selecione a opção Solicitar/Remover Acessos \r\n" +
                                         "2º No campo de busca, digite gitlab e selecione o perfil e o time no qual você pertence, exemplo: \r\n" +
                                         "GITLAB | TECNOLOGIA - HDG - JCORP - PRODUCAO DE CONTEUDO \r\n" +
                                         "3º Após selecionar o perfil, clique em next e depois em Submit \r\n" +
                                         "Digite a letra V para voltar ao inicio";
                }

                if (InfoUsuario.TipoAcesso == Acessos.BancoDados)
                {

                    selecioneOpcoesAcesso = "Para conseguir o acesso ao Banco de Dados, siga caminho abaixo: \r\n" +
                                            "Acesse o link e siga o passo a passo de instalação e configuração https://globo.atlassian.net/wiki/spaces/ENCROS/pages/403526877408/Globo+CLI+Instala+o \r\n" +
                                            "Digite a letra V para voltar ao inicio";
                }                                    

                if (InfoUsuario.TipoAcesso == Acessos.JiraCoFluence)
                {
                    selecioneOpcoesAcesso = "Para conseguir o acesso ao Jira ou Confluence \r\n" +
                                            "procure a SM ou coordenador do seu time! \r\n" +
                                            "Digite a letra V para voltar ao inicio";
                }

                if (InfoUsuario.TipoAcesso == Acessos.GruposTeams)
                {
                    selecioneOpcoesAcesso = "Para conseguir o acesso aos grupos do Teams \r\n" +
                                            "procure a SM ou coordenador do seu time! \r\n" +
                                            "Digite a letra V para voltar ao inicio";
                }

                if (InfoUsuario.TipoAcesso == Acessos.VPN)
                {
                    selecioneOpcoesAcesso = "Para conseguir o acesso a VPN, acesse o link: \r\n" +
                                            "https://globo.atlassian.net/wiki/spaces/JCEDC/pages/403448136247/JCEC+Guia+para+inclus+o+de+novos+colaboradores \r\n" +
                                            "Vá direto na opção (Solicitação de acesso a VPN)";
                }

                await turnContext.SendActivityAsync(MessageFactory.Text(selecioneOpcoesAcesso, selecioneOpcoesAcesso), cancellationToken);
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
