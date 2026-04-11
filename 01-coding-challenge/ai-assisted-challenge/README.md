# Desafio com Assistente de IA — Guia Completo

O segundo módulo do coding challenge (60 minutos) é diferente: você trabalha em um **repositório de código existente** com um **assistente de IA disponível dentro do HackerRank**.

---

## O Que Esperar

- Um repositório com múltiplos arquivos (podem ser 3-10 arquivos)
- Uma tarefa descrita no enunciado (implementar feature, corrigir bug, adicionar função)
- Um assistente de IA embutido no ambiente — **permitido e encorajado usar**
- Acesso à estrutura do projeto, não apenas a um arquivo isolado

---

## Estratégia Geral (60 minutos)

```
0:00 - 0:10  → Ler o enunciado completamente
               Explorar os arquivos do repositório
               Entender a arquitetura e onde mexer

0:10 - 0:20  → Usar o assistente de IA para entender partes obscuras
               Escrever pseudocódigo da solução

0:20 - 0:50  → Implementar a solução
               Usar o assistente para debug e revisão de código

0:50 - 1:00  → Testes, casos especiais, revisão final
```

---

## Como Usar o Assistente de IA com Eficiência

### Boas perguntas para fazer ao assistente:

**Entendimento do código existente:**
- "O que esta função `processOrder()` faz?"
- "Qual é o fluxo de dados entre `FileA` e `FileB`?"
- "Quais são os parâmetros esperados por esta função?"

**Durante a implementação:**
- "Esta implementação de BFS está correta para este problema?"
- "Como posso otimizar esta solução de O(n²) para O(n log n)?"
- "Este caso especial (lista vazia) está sendo tratado?"

**Para debug:**
- "Por que este código retorna X em vez de Y?"
- "Qual pode ser a causa de IndexError nesta linha?"

### O que NÃO fazer:
- Não peça ao assistente para "resolver o problema inteiro" sem entender
- Não copie código cegamente sem ler — você precisa entender para adaptar
- Não confie 100% na IA: revise sempre o código gerado

---

## Lendo um Repositório Desconhecido

Ordem de exploração recomendada:

1. **Leia o README do enunciado** — entenda EXATAMENTE o que precisa fazer
2. **Identifique o ponto de entrada** — main(), solução esperada, função a implementar
3. **Leia os arquivos de teste**, se houver — revelam o comportamento esperado
4. **Trace o fluxo de dados** — onde a entrada entra? onde a saída deve sair?
5. **Identifique onde você deve mexer** — geralmente há um `TODO` ou função vazia

---

## Padrões Comuns Neste Tipo de Desafio

### Implementar uma função dentro de uma classe existente
```python
class GraphSolver:
    def __init__(self, graph):
        self.graph = graph

    def find_shortest_path(self, start, end):
        # TODO: implement this
        pass
```
→ Implemente usando BFS ou Dijkstra, preservando a interface.

### Corrigir um bug
- Procure por `# BUG` ou lógica errada nos loops
- Verifique condições de borda (off-by-one, condição invertida)
- Compare com os testes esperados

### Adicionar tratamento de caso especial
- Leia os casos de teste com atenção
- Adicione verificações no início da função

---

## Checklist antes de Submeter

- [ ] Todos os casos de teste do enunciado passam?
- [ ] A função retorna o tipo correto (lista? inteiro? string?)?
- [ ] Os arquivos que não precisavam de modificação não foram alterados?
- [ ] Os imports necessários estão presentes?
- [ ] A solução funciona para inputs grandes? (não é O(n²) desnecessariamente)

---

## Dica Final

O diferencial do assistente de IA é velocidade de entendimento, não de resolução. Use-o para:
- Entender código que você nunca viu rápido
- Verificar se sua abordagem está correta
- Encontrar bugs mais rápido

A decisão de design, o raciocínio e a validação final ainda são seus.
