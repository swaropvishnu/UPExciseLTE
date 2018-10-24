
$(document).ready(function () {
        

    $.ajax({
        url: '../MenuHandler.ashx',
        method: 'get',
        dataType: 'json',
        success: function (data) {
            buildMenu($('#Sidemenu12'), data);
            //$('#menu').menu();
        }
    });

    function buildMenu(parent, items) {
      
        var li;
        $.each(items, function () {
            for (var i = 0; i < items.length; i++) {
                if($(items[i]).attr('parentId') != null)
                {
                    li = $('<li class ="active" ><a href="' + this.NavURL + '" class="nav-link " >' + this.Icon + '<span class="title">' + this.Text + '</span> </a> </li>');
                }
                else {
                    li = $('<li class ="active"><a href="' + this.NavURL + '" class="nav-link nav-toggle">' + this.Icon + '<span class="title">' + this.Text + '</span></a> </li>');
                }
            }
            //li = $('<li><a href="' + this.NavURL + '" class="dropdown-toggle" data-toggle="dropdown"><i class="icon-list-alt"></i><span>' + this.Text + '</span> </a> </li>');
            if (!this.isActive)
            {
                li.addClass("active");
            }
            li.appendTo(parent);
            if (this.List && this.List.length > 0) {
                //li.removeClass("active");
                li.addClass("menu-dropdown classic-menu-dropdown");
                //$("#li i").remove()
                var ul = $('<ul class="dropdown-menu pull-left"></ul>');
                ul.appendTo(li);
                buildMenu(ul, this.List);
            }
        });
    }
});
   