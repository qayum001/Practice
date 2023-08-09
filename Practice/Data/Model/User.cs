namespace Practice.Data.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime Created { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        /*
         * Вообще иметь все что возможно навигачионные коллекций в сових сущностях плохая идея.
         * Нужно следить за включениями их в запросах
         *      Если ты это делаешь то вероятно в большинстве случаях это избыточно
         *      Если нет то у тебя пустые коллекции, что аналогично отсутствию связаных сущностей,
         *          то есть подкаченная сущность невалидна
         *
         * Чисто для моделей на чтение это правильно
         * Но в доменные сущности проектируются исходя из выполнения бизнесс задач
         *
         * !!! Архитектуру стоит обсудить.
         * Но это большая тема, про которую я сам писать не буду стоит почитать про DDD, чистую архитектуру, слоистую архитектуру и тд
         * Это скорее всего самое важное 
         */


        //Post
        public bool IsAuthor { get; set; } = false;
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        //Like
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        //Comment
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
