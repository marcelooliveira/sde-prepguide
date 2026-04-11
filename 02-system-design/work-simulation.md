# System Design: Work Simulation — Preparação

A simulação de trabalho avalia como você tomaria decisões como SDE II em situações do dia a dia.

---

## Formato Típico

Você receberá um **cenário** e precisará escolher entre opções ou descrever sua abordagem. Exemplos de cenários:

- "Você precisa implementar uma nova feature em um sistema existente. Como você abordaria o design?"
- "O sistema está com alta latência nas consultas. Quais seriam seus próximos passos?"
- "Um novo requisito afeta múltiplos serviços. Como você coordenaria a mudança?"

---

## O Que o Avaliador Espera

| Competência | Exemplo de Comportamento |
|---|---|
| Pensamento sistemático | Quebra o problema em partes menores |
| Consideração de trade-offs | "Esta abordagem tem X vantagem mas Y custo" |
| Foco em impacto | Prioriza soluções com maior valor |
| Comunicação técnica | Explica escolhas de forma clara |
| Consciência de escala | Pensa em como a solução cresce |

---

## Cenários Comuns e Como Abordar

### Cenário: "Seu sistema está lento. O que você faz?"

Resposta estruturada:
1. **Medir antes de agir** — identificar o gargalo com métricas/logs
2. **Hipótese** — banco de dados congestionado? N+1 query? Falta de índice?
3. **Solução proporcional** — cache antes de scale out
4. **Validar** — medir novamente após a mudança

### Cenário: "Como você arquitetaria um sistema de notificações?"

Resposta estruturada:
1. **Clarificar** — push? email? SMS? Quantos usuários?
2. **Componentes** — API de envio, fila de mensagens, workers, banco de logs
3. **Justificar** — fila garante que notificações não se perdem se o worker cair
4. **Trade-offs** — eventual consistency vs. entrega garantida

### Cenário: "Como você lidaria com um requisito ambíguo?"

Resposta estruturada:
1. Clarificaria com o PM/stakeholder as perguntas-chave primeiro
2. Documentaria o que foi decidido
3. Implementaria de forma extensível para acomodar mudanças
4. Entregaria em pequenos incrementos validáveis

---

## Princípios de Design a Demonstrar

### KISS (Keep It Simple, Stupid)
- Prefira a solução mais simples que atende aos requisitos
- Complexidade adicional exige justificativa

### YAGNI (You Aren't Gonna Need It)
- Não implemente funcionalidades especulativas
- Design para o que é necessário hoje, com extensibilidade em mente

### Fail Fast
- Detecte e reporte erros cedo no fluxo
- Prefira crashes óbvios a estados corrompidos silenciosos

### Idempotência
- Operações que podem ser repetidas sem efeitos colaterais adicionais
- Fundamental para sistemas distribuídos com retries

---

## Checklist de Avaliação (do ponto de vista do avaliador)

- [ ] O candidato identificou os requisitos antes de propor soluções?
- [ ] Considerou casos de falha?
- [ ] Justificou as escolhas técnicas?
- [ ] Demonstrou conhecimento de tecnologias comuns (cache, filas, DB)?
- [ ] A solução é realista e escalável?
- [ ] Comunicou trade-offs claramente?
