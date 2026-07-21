add new migration:

```bash
dotnet ef migrations add %NAME% --startup-project WebApi --project Repository
```

run the migrations:

```bash
dotnet ef database update --startup-project WebApi --project Repository
```

register example:

```
mutation {
register(input: {username: "myEditor", password: "pass123Password_"}) {
username
token
expiresAt
}
}
```

query example:

```query {
movies(pagination: {
page: 0,
perPage: 5})
{
items {
id
title
}
}
}```