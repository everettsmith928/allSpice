export class FavoritedRecipe {
  constructor(data){
    this.id = data.id
    this.favoriteId = data.favoriteId
    this.img = data.img
    this.title = data.title
    this.category = data.category
    this.creatorId = data.creatorId
    this.instructions = data.instructions
  }
}