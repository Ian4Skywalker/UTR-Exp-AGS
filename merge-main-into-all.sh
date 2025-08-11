#!/bin/bash

# Guarda la rama actual
current_branch=$(git branch --show-current)

# Cambia a main y actualiza desde remoto
echo "📥 Actualizando main..."
git checkout main
git pull origin main

# Recorre todas las ramas locales excepto main
for branch in $(git branch | sed 's/\*//g' | sed 's/ //g' | grep -v "^main$")
do
  echo "🔁 Cambiando a rama: $branch"
  git checkout "$branch"
  echo "🔄 Fusionando main en $branch..."
  git merge main -m "Merge main into $branch"
done

# Regresa a la rama en la que estabas
git checkout "$current_branch"

echo "✅ Cambios de main fusionados en todas las ramas locales."
