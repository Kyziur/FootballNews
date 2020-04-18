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