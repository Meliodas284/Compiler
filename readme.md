# Лексический анализатор

## 1. Список лексем

| № | Лексема | Пример | Описание |
|---|---------|--------|----------|
| 1 | `ID` | `a`, `abc`, `x1` | Идентификатор |
| 2 | `NUMBER` | `123`, `12.34` | Число (целое или вещественное) |
| 3 | `PLUS` | `+` | Сложение |
| 4 | `MINUS` | `-` | Вычитание |
| 5 | `MUL` | `*` | Умножение |
| 6 | `DIV` | `/` | Деление |
| 7 | `ASSIGN` | `=` | Присваивание |
| 8 | `SEMICOLON` | `;` | Точка с запятой |
| 9 | `COMMA` | `,` | Запятая |
| 10 | `LPAREN` | `(` | Левая круглая скобка |
| 11 | `RPAREN` | `)` | Правая круглая скобка |
| 12 | `LBRACE` | `{` | Левая фигурная скобка |
| 13 | `RBRACE` | `}` | Правая фигурная скобка |
| 14 | `LBRACKET` | `[` | Левая квадратная скобка |
| 15 | `RBRACKET` | `]` | Правая квадратная скобка |
| 16 | `LT` | `<` | Меньше |
| 17 | `GT` | `>` | Больше |
| 18 | `LE` | `<=` | Меньше или равно |
| 19 | `GE` | `>=` | Больше или равно |
| 20 | `EQ` | `==` | Равно |
| 21 | `NE` | `!=` | Не равно |
| 22 | `IF` | `if` | Ключевое слово if |
| 23 | `ELSE` | `else` | Ключевое слово else |
| 24 | `WHILE` | `while` | Ключевое слово while |
| 25 | `READ` | `read` | Ключевое слово read |
| 26 | `WRITE` | `write` | Ключевое слово write |
| 27 | `SQRT` | `sqrt` | Функция квадратного корня |
| 28 | `EXP` | `exp` | Функция экспоненты |
| 29 | `LOG` | `log` | Функция логарифма |
| 30 | `ARRAY` | `array` | Ключевое слово array |
| 31 | `EOF` | — | Конец ввода |

---

## 2. Таблица переходов конечного автомата

### Классы символов

| Обозначение | Описание |
|-------------|----------|
| `<б>` | Буква: a–z, A–Z |
| `<ц>` | Цифра: 0–9 |
| `<.>` | Точка: `.` |
| `<пр>` | Пробел, табуляция, перевод строки |
| `+` `-` `*` `/` `=` `!` `<` `>` `(` `)` `{` `}` `[` `]` `;` `,` | Сами символы |
| `⊥` | Конец ввода (EOF) |
| `other` | Прочие символы (ошибка) |

### Состояния автомата

| Состояние | Описание |
|-----------|----------|
| `S` | Старт |
| `I` | Идентификатор |
| `N` | Число (целая часть) |
| `F` | Число (дробная часть) |
| `A` | Символ `=` |
| `B` | Символ `==` |
| `C` | Символ `!` |
| `D` | Символ `!=` |
| `E` | Символ `<` |
| `G` | Символ `<=` |
| `H` | Символ `>` |
| `K` | Символ `>=` |
| `Z` | Завершение (финальное состояние) |
| `ERR` | Ошибка |

### Таблица переходов

| Состояние ↓ \ Вход → | `<б>` | `<ц>` | `.` | `<пр>` | `+` | `-` | `*` | `/` | `=` | `!` | `<` | `>` | `(` | `)` | `{` | `}` | `[` | `]` | `;` | `,` | `⊥` | `other` |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| `S` | I | N | ERR | S | Z | Z | Z | Z | A | C | E | H | Z | Z | Z | Z | Z | Z | Z | Z | Z | ERR |
| `I` | I | I | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* |
| `N` | Z* | N | F | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* |
| `F` | Z* | F | ERR | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* |
| `A` | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | B | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* |
| `B` | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* |
| `C` | ERR | ERR | ERR | ERR | ERR | ERR | ERR | ERR | D | ERR | ERR | ERR | ERR | ERR | ERR | ERR | ERR | ERR | ERR | ERR | ERR | ERR |
| `D` | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* |
| `E` | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | G | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* |
| `G` | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* |
| `H` | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | K | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* |
| `K` | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* | Z* |

> `Z*` — финальное состояние с возвратом текущего символа во входную ленту (символ принадлежит следующей лексеме).

---

## 3. Семантические программы

| № | Название | Описание действия |
|---|----------|-------------------|
| 0 | **Нет действия** | Просто переход в новое состояние, ничего не делать |
| 1 | **Накопить символ** | Добавить текущий символ к буферу накапливаемой лексемы |
| 2 | **Накопить и вернуть** | Добавить текущий символ к буферу, зафиксировать лексему и вернуть её |
| 3 | **Вернуть без накопления** | Зафиксировать лексему из буфера, текущий символ вернуть во входную ленту (он принадлежит следующей лексеме) |
| 4 | **Определить тип: ID или ключевое слово** | Буфер содержит идентификатор — проверить таблицу ключевых слов; если найдено — вернуть соответствующий номер лексемы (22–30), иначе вернуть `ID` (1) |
| 5 | **Зафиксировать NUMBER** | Буфер содержит число — вернуть лексему `NUMBER` (2) |
| 6 | **Ошибка** | Недопустимый символ в данном состоянии — выдать сообщение об ошибке с номером строки и позицией |
| 7 | **Пропустить пробел** | Текущий символ — пробел / табуляция / перевод строки; буфер не трогать; если `\n` — увеличить счётчик строки |

### Привязка семантических программ к переходам

| Из состояния | Вход | В состояние | Сем. программа | Смысл |
|---|---|---|---|---|
| `S` | `<б>` | `I` | 1 | Начало идентификатора — накопить символ |
| `S` | `<ц>` | `N` | 1 | Начало числа — накопить символ |
| `S` | `<пр>` | `S` | 7 | Пропустить пробел |
| `S` | `+` `-` `*` `/` `(` `)` `{` `}` `[` `]` `;` `,` | `Z` | 2 | Односимвольная лексема — накопить и вернуть |
| `S` | `=` | `A` | 1 | Начало `=` или `==` — накопить |
| `S` | `!` | `C` | 1 | Начало `!=` — накопить |
| `S` | `<` | `E` | 1 | Начало `<` или `<=` — накопить |
| `S` | `>` | `H` | 1 | Начало `>` или `>=` — накопить |
| `I` | `<б>` `<ц>` | `I` | 1 | Продолжение идентификатора — накопить |
| `I` | любой другой | `Z*` | 3 + 4 | Конец идентификатора — вернуть символ, определить тип (ID или ключевое слово) |
| `N` | `<ц>` | `N` | 1 | Продолжение целого числа — накопить |
| `N` | `.` | `F` | 1 | Начало дробной части — накопить |
| `N` | любой другой | `Z*` | 3 + 5 | Конец числа — вернуть символ, зафиксировать NUMBER |
| `F` | `<ц>` | `F` | 1 | Продолжение дробной части — накопить |
| `F` | любой другой | `Z*` | 3 + 5 | Конец вещественного числа — вернуть символ, зафиксировать NUMBER |
| `A` | `=` | `B` | 2 | `=` + `=` → лексема `==` — накопить и вернуть |
| `A` | другой | `Z*` | 3 | Просто `=` — вернуть символ, зафиксировать `ASSIGN` |
| `C` | `=` | `D` | 2 | `!` + `=` → лексема `!=` — накопить и вернуть |
| `C` | другой | `ERR` | 6 | `!` без `=` — ошибка |
| `E` | `=` | `G` | 2 | `<` + `=` → лексема `<=` — накопить и вернуть |
| `E` | другой | `Z*` | 3 | Просто `<` — вернуть символ, зафиксировать `LT` |
| `H` | `=` | `K` | 2 | `>` + `=` → лексема `>=` — накопить и вернуть |
| `H` | другой | `Z*` | 3 | Просто `>` — вернуть символ, зафиксировать `GT` |

---

# КС-грамматика языка

Исходная контекстно-свободная грамматика. 

## Программа

```text
program → statement_list
```

## Список операторов

```text
statement_list → statement statement_list
statement_list → ε
```

## Оператор

```text
statement → assignment
statement → if_statement
statement → while_statement
statement → read_statement
statement → write_statement
statement → block
```

## Составной оператор (блок)

```text
block → LBRACE statement_list RBRACE
```

## Присваивание

```text
assignment → variable ASSIGN expression SEMICOLON
```

## Переменная

```text
variable → ID
variable → ID LBRACKET expression RBRACKET
```

## Условный оператор

```text
if_statement → IF LPAREN condition RPAREN block else_part

else_part → ELSE block
else_part → ε
```

## Оператор цикла

```text
while_statement → WHILE LPAREN condition RPAREN block
```

## Оператор ввода

```text
read_statement → READ LPAREN variable RPAREN SEMICOLON
```

## Оператор вывода

```text
write_statement → WRITE LPAREN expression RPAREN SEMICOLON
```

## Условие

```text
condition → expression rel_op expression
```

## Операции сравнения

```text
rel_op → LT
rel_op → GT
rel_op → LE
rel_op → GE
rel_op → EQ
rel_op → NE
```
## Выражение 

```text
expression → expression PLUS term
expression → expression MINUS term
expression → term
```

## Терм 

```text
term → term MUL factor
term → term DIV factor
term → factor
```

## Множитель

```text
factor → NUMBER
factor → variable
factor → LPAREN expression RPAREN
factor → function_call
```

## Вызов функции

```text
function_call → function_name LPAREN expression RPAREN

function_name → SQRT
function_name → EXP
function_name → LOG
function_name → ARRAY
```

---

# Устранение левой рекурсии

Левая рекурсия возникает когда правило начинается само с себя — парсер уходит в бесконечный цикл ещё до чтения хоть одного токена. В данной грамматике она обнаружена в двух нетерминалах: `expression` и `term`.

## Алгоритм

Правила вида:

```text
A → A α₁
A → A α₂
A → β        ← база (не начинается с A)
```

Заменяются на:

```text
A  → β A'
A' → α₁ A'
A' → α₂ A'
A' → ε
```

Вводится новый нетерминал `A'`, который берёт на себя повторяющийся хвост. База `β` идёт первой — рекурсии больше нет.

---

## Устранение в `expression`

В исходной грамматике:

```text
expression → expression PLUS term    ← левая рекурсия
expression → expression MINUS term   ← левая рекурсия
expression → term                    ← база (β = term)
```

База `term` выносится вперёд, хвосты `PLUS term` и `MINUS term` уходят в `expression'`:

```text
expression  → term expression'
expression' → PLUS term expression'
expression' → MINUS term expression'
expression' → ε
```

---

## Устранение в `term`

В исходной грамматике:

```text
term → term MUL factor    ← левая рекурсия
term → term DIV factor    ← левая рекурсия
term → factor             ← база (β = factor)
```

База `factor` выносится вперёд, хвосты `MUL factor` и `DIV factor` уходят в `term'`:

```text
term  → factor term'
term' → MUL factor term'
term' → DIV factor term'
term' → ε
```

---

# Итоговая грамматика без левой рекурсии

```text
program → statement_list

statement_list → statement statement_list
statement_list → ε

statement → assignment
statement → if_statement
statement → while_statement
statement → read_statement
statement → write_statement
statement → block

block → LBRACE statement_list RBRACE

assignment → variable ASSIGN expression SEMICOLON

variable → ID
variable → ID LBRACKET expression RBRACKET

if_statement → IF LPAREN condition RPAREN block else_part

else_part → ELSE block
else_part → ε

while_statement → WHILE LPAREN condition RPAREN block

read_statement → READ LPAREN variable RPAREN SEMICOLON

write_statement → WRITE LPAREN expression RPAREN SEMICOLON

condition → expression rel_op expression

rel_op → LT | GT | LE | GE | EQ | NE

expression  → term expression'
expression' → PLUS term expression'
expression' → MINUS term expression'
expression' → ε

term  → factor term'
term' → MUL factor term'
term' → DIV factor term'
term' → ε

factor → NUMBER
factor → variable
factor → LPAREN expression RPAREN
factor → function_call

function_call → function_name LPAREN expression RPAREN

function_name → SQRT | EXP | LOG | ARRAY
```

---

# Нестрогая нормальная форма Грейбах (ННФГ)

```text
program → statement_list
program → ε

statement_list → ID statement_list_id_tail statement_list
statement_list → IF LPAREN condition RPAREN block else_part statement_list
statement_list → WHILE LPAREN condition RPAREN block statement_list
statement_list → READ LPAREN ID read_tail RPAREN SEMICOLON statement_list
statement_list → WRITE LPAREN expression RPAREN SEMICOLON statement_list
statement_list → LBRACE statement_list RBRACE statement_list
statement_list → ε

statement_list_id_tail → ASSIGN expression SEMICOLON
statement_list_id_tail → LBRACKET expression RBRACKET ASSIGN expression SEMICOLON

block → LBRACE statement_list RBRACE

else_part → ELSE LBRACE statement_list RBRACE
else_part → ε

read_tail → LBRACKET expression RBRACKET
read_tail → ε

condition → ID condition_id_tail
condition → NUMBER rel_op expression
condition → LPAREN expression RPAREN rel_op expression
condition → SQRT LPAREN expression RPAREN rel_op expression
condition → EXP LPAREN expression RPAREN rel_op expression
condition → LOG LPAREN expression RPAREN rel_op expression
condition → ARRAY LPAREN expression RPAREN rel_op expression

condition_id_tail → LBRACKET expression RBRACKET rel_op expression
condition_id_tail → LT expression
condition_id_tail → GT expression
condition_id_tail → LE expression
condition_id_tail → GE expression
condition_id_tail → EQ expression
condition_id_tail → NE expression

rel_op → LT
rel_op → GT
rel_op → LE
rel_op → GE
rel_op → EQ
rel_op → NE

expression → ID expression_id_tail
expression → NUMBER expression_num_tail
expression → LPAREN expression RPAREN term' expression'
expression → SQRT LPAREN expression RPAREN term' expression'
expression → EXP LPAREN expression RPAREN term' expression'
expression → LOG LPAREN expression RPAREN term' expression'
expression → ARRAY LPAREN expression RPAREN term' expression'

expression_id_tail → LBRACKET expression RBRACKET term' expression'
expression_id_tail → MUL expression_factor_tail expression'
expression_id_tail → DIV expression_factor_tail expression'
expression_id_tail → PLUS term expression'
expression_id_tail → MINUS term expression'
expression_id_tail → ε

expression_num_tail → MUL expression_factor_tail expression'
expression_num_tail → DIV expression_factor_tail expression'
expression_num_tail → PLUS term expression'
expression_num_tail → MINUS term expression'
expression_num_tail → ε

expression_factor_tail → ID factor_id_tail
expression_factor_tail → NUMBER term''
expression_factor_tail → LPAREN expression RPAREN term''
expression_factor_tail → SQRT LPAREN expression RPAREN term''
expression_factor_tail → EXP LPAREN expression RPAREN term''
expression_factor_tail → LOG LPAREN expression RPAREN term''
expression_factor_tail → ARRAY LPAREN expression RPAREN term''

factor_id_tail → LBRACKET expression RBRACKET term''
factor_id_tail → ε

term'' → MUL expression_factor_tail
term'' → DIV expression_factor_tail
term'' → ε

expression' → PLUS term expression'
expression' → MINUS term expression'
expression' → ε

term → ID term_id_tail
term → NUMBER term'
term → LPAREN expression RPAREN term'
term → SQRT LPAREN expression RPAREN term'
term → EXP LPAREN expression RPAREN term'
term → LOG LPAREN expression RPAREN term'
term → ARRAY LPAREN expression RPAREN term'

term_id_tail → LBRACKET expression RBRACKET term'
term_id_tail → MUL term_mul_tail term'
term_id_tail → DIV term_div_tail term'
term_id_tail → ε

term' → MUL term_mul_tail term'
term' → DIV term_div_tail term'
term' → ε

term_mul_tail → ID term_id_base
term_mul_tail → NUMBER
term_mul_tail → LPAREN expression RPAREN
term_mul_tail → SQRT LPAREN expression RPAREN
term_mul_tail → EXP LPAREN expression RPAREN
term_mul_tail → LOG LPAREN expression RPAREN
term_mul_tail → ARRAY LPAREN expression RPAREN

term_div_tail → ID term_id_base
term_div_tail → NUMBER
term_div_tail → LPAREN expression RPAREN
term_div_tail → SQRT LPAREN expression RPAREN
term_div_tail → EXP LPAREN expression RPAREN
term_div_tail → LOG LPAREN expression RPAREN
term_div_tail → ARRAY LPAREN expression RPAREN

term_id_base → LBRACKET expression RBRACKET
term_id_base → ε
```

---

# Семантические действия для генерации ОПС

---

## Обозначения семантических действий

| Символ | Описание |
|--------|----------|
| `□` | Пустое действие — ничего не делать |
| `a` | Записать в ОПС операнд (переменную или константу) из текущей входной лексемы |
| `k` | Записать в ОПС ссылку на константу из таблицы констант |
| `+` | Записать в ОПС операцию сложения |
| `–` | Записать в ОПС операцию вычитания |
| `*` | Записать в ОПС операцию умножения |
| `/` | Записать в ОПС операцию деления |
| `:=` | Записать в ОПС операцию присваивания |
| `–'` | Записать в ОПС операцию **унарного минуса** (отличается от вычитания!) |
| `i` | Записать в ОПС операцию индексирования одномерного массива |
| `<` `>` `<=` `>=` `==` `!=` | Записать в ОПС соответствующую операцию сравнения |
| `r` | Записать в ОПС операцию ввода (read) — операнд должен быть ссылкой на переменную |
| `w` | Записать в ОПС операцию вывода (write) — операнд — числовое значение |
| `1` | Выполнить **семантическую программу 1** (начало условного перехода после условия) |
| `2` | Выполнить **семантическую программу 2** (начало ветки else — безусловный переход) |
| `3` | Выполнить **семантическую программу 3** (конец if-else — заполнение метки) |
| `4` | Выполнить **семантическую программу 4** (фиксация начала цикла) |
| `5` | Выполнить **семантическую программу 5** (конец тела цикла — возврат на начало) |

---

## Семантические программы 1–5

Программы используют **счётчик `k`** — номер очередного генерируемого элемента ОПС, и **магазин меток**.

**Программа 1** (вызывается после генерации условия в `if`):
1. В магазин меток записывается `k`.
2. В ОПС записывается **пустой элемент** — место для будущей метки (адреса перехода при false).
3. В ОПС записывается операция `jf` — переход при условии false.

**Программа 2** (вызывается в начале `else`):
1. По верхнему элементу магазина меток (индекс ранее заготовленного места) записывается `k + 2` — адрес после безусловного перехода.
2. В магазин меток записывается текущее `k`.
3. В ОПС записывается **пустой элемент** — место для будущей метки безусловного перехода.
4. В ОПС записывается операция `j` — безусловный переход (прыжок через ветку `else`).

**Программа 3** (вызывается в конце `if` или `if-else`):
1. По верхнему элементу магазина меток записывается `k` — адрес инструкции, следующей за всем условным оператором.

**Программа 4** (вызывается перед вычислением условия `while`):
1. В магазин меток записывается `k` — адрес начала цикла.

**Программа 5** (вызывается после тела цикла `while`):
1. По верхнему элементу магазина меток записывается `k + 2` — адрес выхода из цикла (куда ведёт `jf`).
2. В ОПС записывается **метка начала цикла** — значение берётся из магазина меток.
3. В ОПС записывается операция `j` — безусловный переход на начало цикла.

---

## Таблица семантических действий по правилам грамматики

### Программа / Список операторов

| Нетерминал | Правая часть | Сем. действия |
|------------|-------------|--------------|
| `program` | `statement_list` | `□` |
| `program` | `ε` | — |
| `statement_list` | `ID statement_list_id_tail statement_list` | `a □ □` |
| `statement_list` | `IF LPAREN condition RPAREN block else_part statement_list` | `4 □ □ 1 □ □ □` |
| `statement_list` | `WHILE LPAREN condition RPAREN block statement_list` | `4 □ □ 1 □ 5 □` |
| `statement_list` | `READ LPAREN ID read_tail RPAREN SEMICOLON statement_list` | `□ □ a □ □ □ □` |
| `statement_list` | `WRITE LPAREN expression RPAREN SEMICOLON statement_list` | `□ □ □ w □ □ □` |
| `statement_list` | `LBRACE statement_list RBRACE statement_list` | `□ □ □ □` |
| `statement_list` | `ε` | — |

> Пояснение: `4` стоит напротив `IF` / `WHILE` (не смешиваем — для `IF` это фиксация начала ветвления через `1`, для `WHILE` — фиксация начала цикла через `4`). Обратите внимание: программа `4` здесь указана условно; для `IF` первым семантическим действием является `□`, а программа `1` вызывается после `RPAREN`. Для `WHILE` программа `4` вызывается на терминале `WHILE`.

Уточнённая таблица с разделением `IF` и `WHILE`:

| Нетерминал | Правая часть | Сем. действия |
|------------|-------------|--------------|
| `statement_list` | `IF LPAREN condition RPAREN block else_part statement_list` | `□ □ □ 1 □ □ □` |
| `statement_list` | `WHILE LPAREN condition RPAREN block statement_list` | `4 □ □ 1 □ 5 □` |

### Хвосты операторов (присваивание)

| Нетерминал | Правая часть | Сем. действия |
|------------|-------------|--------------|
| `statement_list_id_tail` | `ASSIGN expression SEMICOLON` | `□ □ :=` |
| `statement_list_id_tail` | `LBRACKET expression RBRACKET ASSIGN expression SEMICOLON` | `□ □ i □ □ :=` |

### Блок и else

| Нетерминал | Правая часть | Сем. действия |
|------------|-------------|--------------|
| `block` | `LBRACE statement_list RBRACE` | `□ □ □` |
| `else_part` | `ELSE LBRACE statement_list RBRACE` | `2 □ □ 3` |
| `else_part` | `ε` | `3` |

> `2` стоит напротив `ELSE` — в момент встречи `else` генерируем безусловный переход через ветку else. `3` стоит последним в `else_part → ELSE LBRACE statement_list RBRACE` и единственным в `else_part → ε` — завершаем метку.

### Ввод и вывод

| Нетерминал | Правая часть | Сем. действия |
|------------|-------------|--------------|
| `read_tail` | `LBRACKET expression RBRACKET` | `□ □ i` |
| `read_tail` | `ε` | `r` |

> `r` в правиле `read_tail → ε` означает: переменная (с индексом или без) уже помещена в ОПС — генерируем операцию `r`.

### Условие

| Нетерминал | Правая часть | Сем. действия |
|------------|-------------|--------------|
| `condition` | `ID condition_id_tail` | `a □` |
| `condition` | `NUMBER rel_op expression` | `k □ □` |
| `condition` | `LPAREN expression RPAREN rel_op expression` | `□ □ □ □ □` |
| `condition` | `SQRT LPAREN expression RPAREN rel_op expression` | `□ □ □ sqrt □ □` |
| `condition` | `EXP LPAREN expression RPAREN rel_op expression` | `□ □ □ exp □ □` |
| `condition` | `LOG LPAREN expression RPAREN rel_op expression` | `□ □ □ log □ □` |
| `condition` | `ARRAY LPAREN expression RPAREN rel_op expression` | `□ □ □ □ □ □` |
| `condition_id_tail` | `LBRACKET expression RBRACKET rel_op expression` | `□ □ i □ □` |
| `condition_id_tail` | `LT expression` | `< □` |
| `condition_id_tail` | `GT expression` | `> □` |
| `condition_id_tail` | `LE expression` | `<= □` |
| `condition_id_tail` | `GE expression` | `>= □` |
| `condition_id_tail` | `EQ expression` | `== □` |
| `condition_id_tail` | `NE expression` | `!= □` |
| `rel_op` | `LT` | `<` |
| `rel_op` | `GT` | `>` |
| `rel_op` | `LE` | `<=` |
| `rel_op` | `GE` | `>=` |
| `rel_op` | `EQ` | `==` |
| `rel_op` | `NE` | `!=` |

### Выражения

| Нетерминал | Правая часть | Сем. действия |
|------------|-------------|--------------|
| `expression` | `ID expression_id_tail` | `a □` |
| `expression` | `NUMBER expression_num_tail` | `k □` |
| `expression` | `LPAREN expression RPAREN term' expression'` | `□ □ □ □ □` |
| `expression` | `SQRT LPAREN expression RPAREN term' expression'` | `□ □ □ sqrt □ □` |
| `expression` | `EXP LPAREN expression RPAREN term' expression'` | `□ □ □ exp □ □` |
| `expression` | `LOG LPAREN expression RPAREN term' expression'` | `□ □ □ log □ □` |
| `expression` | `ARRAY LPAREN expression RPAREN term' expression'` | `□ □ □ □ □ □` |
| `expression_id_tail` | `LBRACKET expression RBRACKET term' expression'` | `□ □ i □ □` |
| `expression_id_tail` | `MUL expression_factor_tail expression'` | `* □ □` |
| `expression_id_tail` | `DIV expression_factor_tail expression'` | `/ □ □` |
| `expression_id_tail` | `PLUS term expression'` | `□ □ +` |
| `expression_id_tail` | `MINUS term expression'` | `□ □ –` |
| `expression_id_tail` | `ε` | — |
| `expression_num_tail` | `MUL expression_factor_tail expression'` | `* □ □` |
| `expression_num_tail` | `DIV expression_factor_tail expression'` | `/ □ □` |
| `expression_num_tail` | `PLUS term expression'` | `□ □ +` |
| `expression_num_tail` | `MINUS term expression'` | `□ □ –` |
| `expression_num_tail` | `ε` | — |
| `expression'` | `PLUS term expression'` | `□ □ +` |
| `expression'` | `MINUS term expression'` | `□ □ –` |
| `expression'` | `ε` | — |

> Операция `+` / `–` стоит последней, так как в ОПС операция записывается после обоих операндов (постфиксная запись).

### Термы

| Нетерминал | Правая часть | Сем. действия |
|------------|-------------|--------------|
| `term` | `ID term_id_tail` | `a □` |
| `term` | `NUMBER term'` | `k □` |
| `term` | `LPAREN expression RPAREN term'` | `□ □ □ □` |
| `term` | `SQRT LPAREN expression RPAREN term'` | `□ □ □ sqrt □` |
| `term` | `EXP LPAREN expression RPAREN term'` | `□ □ □ exp □` |
| `term` | `LOG LPAREN expression RPAREN term'` | `□ □ □ log □` |
| `term` | `ARRAY LPAREN expression RPAREN term'` | `□ □ □ □ □` |
| `term_id_tail` | `LBRACKET expression RBRACKET term'` | `□ □ i □` |
| `term_id_tail` | `MUL term_mul_tail term'` | `* □ □` |
| `term_id_tail` | `DIV term_div_tail term'` | `/ □ □` |
| `term_id_tail` | `ε` | — |
| `term'` | `MUL term_mul_tail term'` | `□ □ *` |
| `term'` | `DIV term_div_tail term'` | `□ □ /` |
| `term'` | `ε` | — |
| `term_mul_tail` | `ID term_id_base` | `a □` |
| `term_mul_tail` | `NUMBER` | `k` |
| `term_mul_tail` | `LPAREN expression RPAREN` | `□ □ □` |
| `term_mul_tail` | `SQRT LPAREN expression RPAREN` | `□ □ □ sqrt` |
| `term_mul_tail` | `EXP LPAREN expression RPAREN` | `□ □ □ exp` |
| `term_mul_tail` | `LOG LPAREN expression RPAREN` | `□ □ □ log` |
| `term_mul_tail` | `ARRAY LPAREN expression RPAREN` | `□ □ □ □` |
| `term_div_tail` | `ID term_id_base` | `a □` |
| `term_div_tail` | `NUMBER` | `k` |
| `term_div_tail` | `LPAREN expression RPAREN` | `□ □ □` |
| `term_div_tail` | `SQRT LPAREN expression RPAREN` | `□ □ □ sqrt` |
| `term_div_tail` | `EXP LPAREN expression RPAREN` | `□ □ □ exp` |
| `term_div_tail` | `LOG LPAREN expression RPAREN` | `□ □ □ log` |
| `term_div_tail` | `ARRAY LPAREN expression RPAREN` | `□ □ □ □` |
| `term_id_base` | `LBRACKET expression RBRACKET` | `□ □ i` |
| `term_id_base` | `ε` | — |

> Операции `*` и `/` в `term'` стоят **последними** — после обоих операндов (постфиксная форма).

---

# Список операций ОПС

| № | Операция | Обозначение | Арность | Описание |
|---|----------|-------------|---------|----------|
| 1 | Сложение | `+` | 2 | Извлечь два операнда, сложить, результат в магазин |
| 2 | Вычитание | `–` | 2 | Извлечь два операнда, вычесть, результат в магазин |
| 3 | Умножение | `*` | 2 | Извлечь два операнда, перемножить, результат в магазин |
| 4 | Деление | `/` | 2 | Извлечь два операнда, разделить, результат в магазин |
| 5 | Унарный минус | `–'` | 1 | Извлечь один операнд, изменить знак, результат в магазин |
| 6 | Присваивание | `:=` | 2 | Первый операнд — ссылка на переменную; второй — значение. Записать значение по ссылке. В магазин ничего не кладётся |
| 7 | Индексирование одномерного массива | `i` | 2 | Первый операнд — ссылка на паспорт массива; второй — значение индекса. Результат — ссылка на элемент `M + d*k`. Записать ссылку в магазин |
| 8 | Меньше | `<` | 2 | Сравнить два операнда, результат `true`/`false` в магазин |
| 9 | Больше | `>` | 2 | Аналогично |
| 10 | Меньше или равно | `<=` | 2 | Аналогично |
| 11 | Больше или равно | `>=` | 2 | Аналогично |
| 12 | Равно | `==` | 2 | Аналогично |
| 13 | Не равно | `!=` | 2 | Аналогично |
| 14 | Условный переход (по false) | `jf` | 2 | Извлечь `bool` (1-й) и метку (2-й). Если `bool = false` — перейти на метку. Если `true` — продолжить. В магазин ничего не кладётся |
| 15 | Безусловный переход | `j` | 1 | Извлечь метку, перейти на неё. В магазин ничего не кладётся |
| 16 | Ввод | `r` | 1 | Извлечь ссылку на переменную, прочитать значение со стандартного ввода, записать по ссылке. В магазин ничего не кладётся |
| 17 | Вывод | `w` | 1 | Извлечь значение из магазина, вывести на стандартный вывод. В магазин ничего не кладётся |
| 18 | Квадратный корень | `sqrt` | 1 | Извлечь операнд, вычислить `√x`, результат в магазин |
| 19 | Экспонента | `exp` | 1 | Извлечь операнд, вычислить `eˣ`, результат в магазин |
| 20 | Натуральный логарифм | `log` | 1 | Извлечь операнд, вычислить `ln(x)`, результат в магазин |

---

# Формат ОПС

## Структура элемента ОПС

ОПС — линейный массив элементов. Каждый элемент:

```
┌──────────┬────────────────────────────────────────────────────────┐
│   type   │  value                                                 │
├──────────┼────────────────────────────────────────────────────────┤
│  (enum)  │  (union: ссылка в таблицу / значение / код операции)  │
└──────────┴────────────────────────────────────────────────────────┘
```

### Поле `type` — тип элемента

| Значение | Описание |
|----------|----------|
| `TYPE_VAR` | Операнд — ссылка на переменную в таблице переменных |
| `TYPE_CONST` | Операнд — ссылка на константу в таблице констант |
| `TYPE_LABEL` | Операнд — метка (номер элемента ОПС, адрес перехода) |
| `TYPE_OP` | Операция (код из списка операций ОПС) |

### Поле `value` — значение элемента

| `type` | Содержимое `value` | Пример |
|--------|-------------------|--------|
| `TYPE_VAR` | Индекс в таблице переменных | `2` (третья переменная) |
| `TYPE_CONST` | Индекс в таблице констант | `0` (первая константа) |
| `TYPE_LABEL` | Целое число — номер элемента ОПС | `7` |
| `TYPE_OP` | Код операции (OpCode) | `OP_ADD`, `OP_JF`, `OP_I` |

### Виды содержимого в магазине интерпретатора

| Вид | Описание |
|-----|----------|
| Ссылка на переменную | Адрес в таблице скалярных переменных |
| Ссылка на константу | Адрес в таблице констант |
| Числовое значение | Результат арифметической операции |
| Ссылка на массив | Адрес паспорта массива |
| Ссылка на элемент массива | Вычисленный адрес `M + d*k` (результат операции `i`) |
