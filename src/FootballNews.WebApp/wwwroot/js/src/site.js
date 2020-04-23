function areYouSure() {
    const deleteLinks = document.querySelectorAll('.delete');
    for (let i = 0; i < deleteLinks.length; i++) {
        deleteLinks[i].addEventListener('click', function (event) {
            event.preventDefault();
            console.log(this.getAttribute('data-confirm'));
            const choice = confirm(this.getAttribute('data-confirm'));

            if (choice) {
                const url = this.getAttribute('href');
                $.ajax({
                    url: `${url}`,
                    type: 'DELETE',
                    success: function (result) {
                        console.log('Successfully deleted item.');
                        location.reload();
                    },
                    fail: function (result) {
                        console.log(`Error while deleting item on ${url}. Error msg: ${result}`);
                    }
                });
            }
        });
    }
}

async function getPlayers(team) {
    let result;
    try {
        result = $.ajax({
            url: `/admin/players/getbyteam/?team=${team}`,
            type: 'GET',
        });

        return result;
    } catch (error) {
        console.log(error);
    }
    
}

function mapModelToComment(data){
    let model;
    console.log('received data:', data);
    if(Array.isArray(data)){
        model = data.map(comment => ({
            id: comment.Id,
            created: comment.CreatedDate,
            content: comment.Text,
            modified: comment.UpdatedDate,
            fullname: comment.FullName,
            parent: comment.ParentId === undefined || comment.ParentId === null ? null : comment.ParentId,
            createdByCurrentUser: comment.CreatedByCurrentUser,
            // createdByAdmin: comment.CreatedByAdmin,
            currentUserIsAdmin: comment.CurrentUserIsAdmin
        }));
        return model;
    }else{
        model = {
            id: data.Id,
            created: data.CreatedDate,
            content: data.Text,
            modified: data.UpdatedDate,
            fullname: data.FullName,
            createdByCurrentUser: data.CreatedByCurrentUser,
            // createdByAdmin: data.CreatedByAdmin,
            currentUserIsAdmin: data.CurrentUserIsAdmin,
            parent: data.ParentId === undefined || data.ParentId === null ? null : data.ParentId,
        }
        
        return model;
    }
    
    return model;
}